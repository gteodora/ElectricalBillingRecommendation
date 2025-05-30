using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Repositories.Interfaces;

public interface IPlanService
{
    Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken);
    Task<Plan?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken);
    Task<Plan?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Plan plan, CancellationToken cancellationToken);
    void Update(Plan plan);
    void Delete(Plan plan);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}

