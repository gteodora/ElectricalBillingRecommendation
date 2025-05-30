using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Repositories.Interfaces;

public interface ITaxGroupService
{
    Task<IEnumerable<TaxGroup>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaxGroup?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken);
    Task<TaxGroup?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(TaxGroup taxGroup, CancellationToken cancellationToken); 
    void Update(TaxGroup taxGroup);
    void Delete(TaxGroup taxGroup);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
