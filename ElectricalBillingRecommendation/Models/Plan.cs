namespace ElectricalBillingRecommendation.Models;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public DateTime UpdatedAt { get; set; }
}

