using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.PricingTier;

public class PricingTierReadDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Threshold must be positive.")]
    public int Threshold { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "PricePerKwh must be greater than 0.")]
    public double PricePerKwh { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; }

    public int PlanId { get; set; }
}


