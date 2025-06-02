using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.TaxGroup;

    public class TaxGroupCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } 

        [Range(0, 1)]
        public double Vat { get; set; }

        [Range(0, 1)]
        public double EcoTax { get; set; }
    }

