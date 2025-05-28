using ElectricalBillingRecommendation.Dtos.PricingTier;

namespace ElectricalBillingRecommendation.Services.Interfaces;

public interface IPricingTierService
{
    Task<IEnumerable<PricingTierReadDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PricingTierReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PricingTierReadDto> CreateAsync(PricingTierCreateDto pricingTierCreateDto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, PricingTierUpdateDto pricingTierUpdateDto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
