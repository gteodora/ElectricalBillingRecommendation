using AutoMapper;
using ElectricalBillingRecommendation.Dtos.Plan;
using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Mappings;

public class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Plan, PlanReadDto>();
        CreateMap<PlanCreateDto, Plan>();
        CreateMap<PlanUpdateDto, Plan>();
    }
}
