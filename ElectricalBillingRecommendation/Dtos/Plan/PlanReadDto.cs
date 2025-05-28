using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.Plan;

public class PlanReadDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Range(0, 1)]
    public double Discount { get; set; }
}

