using ElectricalBillingRecommendation.Dtos.Plan;

namespace ElectricalBillingRecommendation.Services.Interfaces;

public interface IPlanService
{
    Task<IEnumerable<PlanReadDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PlanReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PlanReadDto> CreateAsync(PlanCreateDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, PlanUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

