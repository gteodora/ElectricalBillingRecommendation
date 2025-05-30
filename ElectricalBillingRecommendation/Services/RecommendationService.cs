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

        //izbaci planove u kojima nema pricingTier-a:
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

        Recommendation calculatedRecommendation = new Recommendation(taxGroup, averageMonthlyConsumption);

        var planPrices = plans
            .Select(p => CalculatePlanPrice(p, averageMonthlyConsumption))
            .ToList();

        var totalPrices = planPrices
            .Select(planPrice => planPrice += planPrice * (taxGroup.EcoTax + taxGroup.Vat))
            .ToList();

        var taxes = planPrices
            .Select(planPrices => planPrices * (taxGroup.EcoTax + taxGroup.Vat))
            .ToList();

        

        var lowestTotalPrice = totalPrices.Min();
        var indexOfLowestTotalPrice = totalPrices.IndexOf(lowestTotalPrice);

        calculatedRecommendation.TotalCost = lowestTotalPrice;
        //treba vidjeti ima li vise planova iste cijene
        //calculatedRecommendation.LowestPricePlans = (new List<Plan>()).Add(plans.ToList().[indexOfLowestTotalPrice]);
        var plansList = plans.ToList();
        calculatedRecommendation.LowestPricePlan = plansList[indexOfLowestTotalPrice];
        calculatedRecommendation.TaxCost = taxes[indexOfLowestTotalPrice];

        return calculatedRecommendation;
    }



    private double CalculatePlanPrice(PlanReadDto plan, int averageMonthlyConsumption)
    {
        /*if (plan.PricingTiers != null 
            && plan.PricingTiers.Count > 0) //ako ima pricing tierova u planu
        {*/

            var pricingTier = plan.PricingTiers
                .OrderBy(pricingTier => pricingTier.Threshold ?? int.MaxValue)
                .FirstOrDefault(pricingTier => pricingTier.Threshold >= averageMonthlyConsumption
                                                || pricingTier.Threshold == null);
            

            //if (pricingTier != null 
            //    || plan.PricingTiers.Any(pricingTier => pricingTier.Threshold == null))
           // {
                return (pricingTier.PricePerKwh * averageMonthlyConsumption) * (1 - plan.Discount);
           // }
        //}

       // return 0;
    }
}