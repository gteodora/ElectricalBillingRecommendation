using AutoMapper;
using ElectricalBillingRecommendation.Dtos.Plan;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using ElectricalBillingRecommendation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectricalBillingRecommendation.Services;

public class PlanService : IPlanService
{
    private readonly IPlanRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<TaxGroupService> _logger;

    public PlanService(IPlanRepository repository, IMapper mapper, ILogger<TaxGroupService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PlanReadDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var plans = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PlanReadDto>>(plans);
    }

    public async Task<PlanReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdReadOnlyAsync(id, cancellationToken);

        if (plan == null)
        {
            _logger.LogWarning("Plan with ID {Id} not found.", id);
            return null;
        }

        return _mapper.Map<PlanReadDto>(plan);
    }

    public async Task<PlanReadDto> CreateAsync(PlanCreateDto planCreateDto, CancellationToken cancellationToken)
    {
        var newPlan = _mapper.Map<Plan>(planCreateDto);
        newPlan.UpdatedAt = DateTime.UtcNow;

        await _repository.AddAsync(newPlan, cancellationToken);

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created new Plan with ID {Id}", newPlan.Id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error occurred while saving new Plan.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving new Plan.");
            throw;
        }

        return _mapper.Map<PlanReadDto>(newPlan);
    }

    public async Task<bool> UpdateAsync(int id, PlanUpdateDto planUpdateDto, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdTrackedAsync(id, cancellationToken);
        if (plan == null)
            return false;

        if (!string.IsNullOrWhiteSpace(planUpdateDto.Name))
            plan.Name = planUpdateDto.Name;

        if (planUpdateDto.Discount.HasValue)
            plan.Discount = planUpdateDto.Discount.Value;

        plan.UpdatedAt = DateTime.UtcNow;

        _repository.Update(plan);

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict updating Plan with Id {PlanId}", id);
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error updating Plan with Id {PlanId}", id);
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex, "Request was canceled while updating Plan with Id {PlanId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when updating Plan with Id {PlanId}", id);
            throw;
        }

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdTrackedAsync(id, cancellationToken);
        if (plan == null)
        {
            _logger.LogWarning("Attempted to delete Plan with Id {PlanId}, but it was not found.", id);
            return false;
        }

        _repository.Delete(plan);

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleted Plan with Id {PlanId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting TaxGroup with Id {PlanId}", id);
            throw;
        }
    }
}

