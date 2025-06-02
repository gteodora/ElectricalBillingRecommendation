using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.TaxGroup;

    public class TaxGroupReadDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaxGroup { get; set; }

        [Range(0, 1)]
        public double Vat { get; set; }

        [Range(0, 1)]
        public double EcoTax { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } 
    }

