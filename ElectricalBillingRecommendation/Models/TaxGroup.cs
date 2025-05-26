namespace ElectricalBillingRecommendation.Models;

public class TaxGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Vat { get; set; }
    public double EcoTax { get; set; }
    public DateTime UpdatedAt { get; set; }
}

