using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Dtos.Plan;

namespace ElectricalBillingRecommendation.Models;

public class Recommendation
{
    public int Consumption { get; set; }
    public double TotalCost { get; set; }
    public double TaxCost { get; set; }
    public TaxGroupReadDto TaxGroup { get; set; }
    //public List<PlanReadDto> LowestPricePlans { get; set; }
    public PlanReadDto LowestPricePlan { get; set; }


    public Recommendation(TaxGroupReadDto taxGroup, int averageMonthlyConsumption)
    {
        TaxGroup = taxGroup;
        Consumption = averageMonthlyConsumption;
    }
}

