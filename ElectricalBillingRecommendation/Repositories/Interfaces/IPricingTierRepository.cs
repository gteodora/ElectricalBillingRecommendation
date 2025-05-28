using ElectricalBillingRecommendation.Dtos.PricingTier;
using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Repositories.Interfaces;

public interface IPricingTierRepository
{
    Task<IEnumerable<PricingTier>> GetAllAsync(CancellationToken cancellationToken);
    Task<PricingTier?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken);
    Task<PricingTier?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(PricingTier tier, CancellationToken cancellationToken);
    void Update(PricingTier pricingTier);
    void Delete(PricingTier pricingTier);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}