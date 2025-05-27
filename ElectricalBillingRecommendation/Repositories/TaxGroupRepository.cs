using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ElectricalBillingRecommendation.Repositories;

public class TaxGroupRepository : ITaxGroupRepository
{
    private readonly AppDbContext _context;

    public TaxGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaxGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.TaxGroups
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    // Koristi se za GET – ne treba EF da prati entitet
    public async Task<TaxGroup?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TaxGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(tg => tg.Id == id, cancellationToken);
    }

    // Koristi se za UPDATE, DELETE – EF mora da prati entitet
    public async Task<TaxGroup?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TaxGroups
            .FirstOrDefaultAsync(tg => tg.Id == id, cancellationToken);
    }

    public async Task AddAsync(TaxGroup taxGroup, CancellationToken cancellationToken)
    {
        await _context.TaxGroups.AddAsync(taxGroup, cancellationToken);
    }

    public void Update(TaxGroup taxGroup)
    {
        _context.TaxGroups.Update(taxGroup);
    }

    public void Delete(TaxGroup taxGroup)
    {
        _context.TaxGroups.Remove(taxGroup);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}

