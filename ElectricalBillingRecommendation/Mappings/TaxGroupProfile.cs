using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Models;
using AutoMapper;

namespace ElectricalBillingRecommendation.Mappings;

public class TaxGroupProfile : Profile
{
    public TaxGroupProfile()
    {
        // Mapiranje iz entiteta u DTO
        CreateMap<TaxGroup, TaxGroupReadDto>()
            .ForMember(dest => dest.TaxGroup, opt => opt.MapFrom(src => src.Name));

        // Mapiranje iz Create i Update DTO u entitet
        CreateMap<TaxGroupCreateDto, TaxGroup>();
        CreateMap<TaxGroupUpdateDto, TaxGroup>();
    }
}

