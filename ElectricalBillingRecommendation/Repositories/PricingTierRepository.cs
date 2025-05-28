using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ElectricalBillingRecommendation.Repositories;

public class PricingTierRepository : IPricingTierRepository
{
    private readonly AppDbContext _context;

    public PricingTierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PricingTier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.PricingTiers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PricingTier?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.PricingTiers
            .AsNoTracking()
            .FirstOrDefaultAsync(pt => pt.Id == id, cancellationToken);
    }

    public async Task<PricingTier?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.PricingTiers
            .FirstOrDefaultAsync(pt => pt.Id == id, cancellationToken);
    }

    public async Task CreateAsync(PricingTier pricingTier, CancellationToken cancellationToken)
    {
        await _context.PricingTiers.AddAsync(pricingTier, cancellationToken);
    }

    public void Update(PricingTier pricingTier)
    {
        _context.PricingTiers.Update(pricingTier);
    }

    public void Delete(PricingTier pricingTier)
    {
        _context.PricingTiers.Remove(pricingTier);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}

