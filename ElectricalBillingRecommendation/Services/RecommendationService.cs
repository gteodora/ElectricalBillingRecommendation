using ElectricalBillingRecommendation.Dtos.Plan;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Services.Interfaces;

namespace ElectricalBillingRecommendation.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IPlanService _planService;
    private readonly ITaxGroupService _taxGroupService;
    private readonly ILogger<RecommendationService> _logger;

    public RecommendationService(IPlanService planService, ITaxGroupService taxGroupService, ILogger<RecommendationService> logger)
    {
        _planService = planService;
        _taxGroupService = taxGroupService;
        _logger = logger;
    }

    public async Task<Recommendation?> GetAllAsync(int taxGroupId, int averageMonthlyConsumption, CancellationToken cancellationToken)
    {
        var plans = await _planService.GetAllAsync(cancellationToken);
        var taxGroup = await _taxGroupService.GetByIdTaxGroupAsync(taxGroupId, cancellationToken);

        if (plans == null || !plans.Any()) 
        {
            _logger.LogWarning("There does not exist any plan.");
            return null;
        }

        plans = plans.Where(plan => plan.PricingTiers != null 
                                   && plan.PricingTiers.Count() > 0
                                   && plan.PricingTiers.Any(pricingTier => pricingTier.Threshold >= averageMonthlyConsumption
                                                                        || pricingTier.Threshold == null))
                    .ToList();

        if (!plans.Any()) 
        {
            _logger.LogWarning("There does not exist any plan or suitable plan.");
            return null;
        }

        var taxRate = taxGroup.EcoTax + taxGroup.Vat;

        var planSummaries = plans.Select(plan =>
        {
            var basePrice = CalculatePlanPrice(plan, averageMonthlyConsumption);
            var planDiscount = basePrice * plan.Discount;
            var taxAmount = basePrice * taxRate;
            var totalPrice = basePrice - planDiscount + taxAmount;

            return new PlanSummary
            {
                Plan = plan,
                BasePrice = basePrice,
                PlanDiscount = planDiscount,
                TaxAmount = taxAmount,
                TotalPrice = totalPrice
            };
        }).ToList();

        var lowestTotalPrice = planSummaries.Min(p => p.TotalPrice);
        var bestPlans = planSummaries
            .Where(p => p.TotalPrice == lowestTotalPrice)
            .ToList();

        return new Recommendation(taxGroup, averageMonthlyConsumption, bestPlans);
    }

    private double CalculatePlanPrice(PlanReadDto plan, int averageMonthlyConsumption)
    {
            var pricingTier = plan.PricingTiers
                .OrderBy(pricingTier => pricingTier.Threshold ?? int.MaxValue)
                .FirstOrDefault(pricingTier => pricingTier.Threshold >= averageMonthlyConsumption
                                                || pricingTier.Threshold == null);
            
                return pricingTier.PricePerKwh * averageMonthlyConsumption;
    }
}


