using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.Plan;

public class PlanUpdateDto
{
    [Required]
    [MaxLength(255)]
    public string? Name { get; set; } //= default;

    [Range(0, 1)]
    public double? Discount { get; set; }
}

