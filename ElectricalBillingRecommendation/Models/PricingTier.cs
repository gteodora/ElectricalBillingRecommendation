namespace ElectricalBillingRecommendation.Models;

public class PricingTier
{
    public int Id { get; set; }
    public int? Threshold { get; set; }
    public double PricePerKwh { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int PlanId { get; set; }

    public Plan Plan { get; set; }
}

