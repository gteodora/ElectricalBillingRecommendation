﻿using System.ComponentModel.DataAnnotations;

namespace ElectricalBillingRecommendation.Dtos.Plan;

public class PlanCreateDto
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Range(0, 1)]
    public double Discount { get; set; }
}

