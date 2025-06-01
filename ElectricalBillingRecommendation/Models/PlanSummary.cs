using ElectricalBillingRecommendation.Dtos.Plan;

namespace ElectricalBillingRecommendation.Models;

public class PlanSummary
{
    public double BasePrice {  get; set; }
    public double PlanDiscount { get; set; }
    public double TaxAmount {  get; set; }
    public double TotalPrice { get; set; }
    public PlanReadDto Plan { get; set; }
}


