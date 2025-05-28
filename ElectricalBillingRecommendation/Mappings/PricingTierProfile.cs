using AutoMapper;
using ElectricalBillingRecommendation.Dtos.PricingTier;
using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Mappings;

public class PricingTierProfile : Profile
{
    public PricingTierProfile()
    {
        CreateMap<PricingTier, PricingTierReadDto>();
        CreateMap<PricingTierCreateDto, PricingTier>();
        CreateMap<PricingTierUpdateDto, PricingTier>();
    }
}

