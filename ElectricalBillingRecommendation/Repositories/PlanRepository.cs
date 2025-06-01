using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectricalBillingRecommendation.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly AppDbContext _context;

    public PlanRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Plans
            .Include(plans => plans.PricingTiers)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Plan?> GetByIdReadOnlyAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Plans
            .Include(plans => plans.PricingTiers)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Plan?> GetByIdTrackedAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Plans
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Plan plan, CancellationToken cancellationToken)
    {
        await _context.Plans.AddAsync(plan, cancellationToken);
    }

    public void Update(Plan plan)
    {
        _context.Plans.Update(plan);
    }

    public void Delete(Plan plan)
    {
        _context.Plans.Remove(plan);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    
}
