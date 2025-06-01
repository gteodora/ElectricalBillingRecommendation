using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Dtos.Plan;

namespace ElectricalBillingRecommendation.Models;

public class Recommendation
{
    public int Consumption { get; set; }
    public TaxGroupReadDto TaxGroup { get; set; }
    public List<PlanSummary> PlanSummaries { get; set; }


    public Recommendation(TaxGroupReadDto taxGroup, int averageMonthlyConsumption, List<PlanSummary> planSummaries)
    {
        TaxGroup = taxGroup;
        Consumption = averageMonthlyConsumption;
        PlanSummaries = planSummaries;
    }
}

