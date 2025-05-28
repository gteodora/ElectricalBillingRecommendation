using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.PricingTier;

public class PricingTierUpdateDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Threshold must be positive.")]
    public int? Threshold { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "PricePerKwh must be greater than 0.")]
    public double? PricePerKwh { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "PlanId must be positive.")]
    public int PlanId { get; set; }
}

