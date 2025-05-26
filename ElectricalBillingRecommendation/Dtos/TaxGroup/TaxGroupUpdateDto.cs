using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.TaxGroup;

    public class TaxGroupUpdateDto
    {
        [MaxLength(255)]
        public string? Name { get; set; }

        [Range(0, 1)]
        public double? Vat { get; set; }

        [Range(0, 1)]
        public double? EcoTax { get; set; }
    }

