using AutoMapper;
using ElectricalBillingRecommendation.Dtos.PricingTier;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using ElectricalBillingRecommendation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectricalBillingRecommendation.Services;

public class PricingTierService : IPricingTierService
{
    private readonly IPricingTierRepository _pricingTierRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PricingTierService> _logger;

    public PricingTierService(IPricingTierRepository pricingTierRepository, IPlanRepository planRepository, IMapper mapper, ILogger<PricingTierService> logger)
    {
        _pricingTierRepository = pricingTierRepository;
        _planRepository = planRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PricingTierReadDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var pricingTiers = await _pricingTierRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<PricingTierReadDto>>(pricingTiers);
    }

    public async Task<PricingTierReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var pricingTier = await _pricingTierRepository.GetByIdReadOnlyAsync(id, cancellationToken);

        if (pricingTier == null)
        {
            _logger.LogWarning("PricingTier with ID {Id} not found.", id);
            return null;
        }

        return _mapper.Map<PricingTierReadDto>(pricingTier);
    }

    public async Task<PricingTierReadDto> CreateAsync(PricingTierCreateDto pricingTierCreateDto, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdTrackedAsync(pricingTierCreateDto.PlanId, cancellationToken);
        if (plan == null)
        {
            throw new ArgumentException($"Plan with Id {pricingTierCreateDto.PlanId} does not exist.");
        }

        var newPricingTier = _mapper.Map<PricingTier>(pricingTierCreateDto);
        newPricingTier.UpdatedAt = DateTime.UtcNow;
        newPricingTier.PlanId = pricingTierCreateDto.PlanId;

        await _pricingTierRepository.CreateAsync(newPricingTier, cancellationToken);

        try
        {
            await _pricingTierRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created new PricingTier with ID {Id}", newPricingTier.Id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error occurred while saving new PricingTier.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving new PricingTier.");
            throw;
        }

        return _mapper.Map<PricingTierReadDto>(newPricingTier);
    }

    public async Task<bool> UpdateAsync(int id, PricingTierUpdateDto pricingTierUpdateDto, CancellationToken cancellationToken)
    {
        var pricingTier = await _pricingTierRepository.GetByIdTrackedAsync(id, cancellationToken);

        if (pricingTier == null)
            return false;

        if (pricingTierUpdateDto.Threshold.HasValue)
            pricingTier.Threshold = pricingTierUpdateDto.Threshold.Value;

        if (pricingTierUpdateDto.PricePerKwh.HasValue)
            pricingTier.PricePerKwh = pricingTierUpdateDto.PricePerKwh.Value;

        pricingTier.UpdatedAt = DateTime.UtcNow;

        _pricingTierRepository.Update(pricingTier);

        try
        {
            await _pricingTierRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict updating PricingTier with Id {PricingTierId}", id);
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error updating PricingTier with Id {PricingTierId}", id);
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex, "Request was canceled while updating PricingTier with Id {PricingTierId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when updating PricingTier with Id {PricingTierId}", id);
            throw;
        }

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var pricingTier = await _pricingTierRepository.GetByIdTrackedAsync(id, cancellationToken);
        if (pricingTier == null)
        {
            _logger.LogWarning("Attempted to delete PricingTier with ID {Id}, but it was not found.", id);
            return false;
        }

        _pricingTierRepository.Delete(pricingTier);

        try
        {
            await _pricingTierRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleted PricingTier with ID {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting PricingTier with ID {Id}", id);
            throw;
        }
    }
}

