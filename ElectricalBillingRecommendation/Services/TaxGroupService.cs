using AutoMapper;
using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Repositories;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using ElectricalBillingRecommendation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace ElectricalBillingRecommendation.Services;

public class TaxGroupService : ITaxGroupService
{
    private readonly AppDbContext _context;
    private readonly ITaxGroupRepository _taxGroupRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TaxGroupService> _logger;

    public TaxGroupService(AppDbContext context, IMapper mapper, ITaxGroupRepository taxGroupRepository, ILogger<TaxGroupService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _taxGroupRepository = taxGroupRepository;
    }

    public async Task<IEnumerable<TaxGroupReadDto>> GetAllTaxGroupAsync(CancellationToken cancellationToken)
    {
        var taxGroups = await _taxGroupRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TaxGroupReadDto>>(taxGroups);
    }

    public async Task<TaxGroupReadDto?> GetByIdTaxGroupAsync(int id, CancellationToken cancellationToken)
    {
        var taxGroup = await _taxGroupRepository.GetByIdReadOnlyAsync(id, cancellationToken);

        if (taxGroup == null)
        {
            _logger.LogWarning("TaxGroup with ID {Id} not found.", id);
            return null;
        }

        return _mapper.Map<TaxGroupReadDto>(taxGroup);
    }

    public async Task<bool> UpdateTaxGroupAsync(int id, TaxGroupUpdateDto taxGroupUpdateDto, CancellationToken cancellationToken)
    {
        var taxGroup = await _taxGroupRepository.GetByIdTrackedAsync(id, cancellationToken);

        if (taxGroup == null)
            return false;

        if (taxGroupUpdateDto.Name != null)
            taxGroup.Name = taxGroupUpdateDto.Name;

        if (taxGroupUpdateDto.Vat.HasValue)
            taxGroup.Vat = taxGroupUpdateDto.Vat.Value;

        if (taxGroupUpdateDto.EcoTax.HasValue)
            taxGroup.EcoTax = taxGroupUpdateDto.EcoTax.Value;

        taxGroup.UpdatedAt = DateTime.UtcNow;

        _taxGroupRepository.Update(taxGroup);
        try
        {
            await _taxGroupRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict updating TaxGroup with Id {TaxGroupId}", id);
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error updating TaxGroup with Id {TaxGroupId}", id);
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex, "Request was canceled while updating TaxGroup with Id {TaxGroupId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when updating TaxGroup with Id {TaxGroupId}", id);
            throw;
        }

        return true;
    }

    public async Task<TaxGroupReadDto> CreateTaxGroupAsync(TaxGroupCreateDto taxGroupCreateDto, CancellationToken cancellationToken)
    {
        var newTaxGroup = _mapper.Map<TaxGroup>(taxGroupCreateDto);
        newTaxGroup.UpdatedAt = DateTime.UtcNow;

        await _taxGroupRepository.AddAsync(newTaxGroup, cancellationToken);

        try
        {
            await _taxGroupRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created new TaxGroup with ID {Id}", newTaxGroup.Id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error occurred while saving new TaxGroup.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving new TaxGroup.");
            throw;
        }

        return _mapper.Map<TaxGroupReadDto>(newTaxGroup);
    }

    public async Task<bool> DeleteTaxGroupAsync(int id, CancellationToken cancellationToken)
    {
        var taxGroup = await _taxGroupRepository.GetByIdTrackedAsync(id, cancellationToken);
        if (taxGroup == null)
        {
            _logger.LogWarning("Attempted to delete TaxGroup with ID {Id}, but it was not found.", id);
            return false;
        }

        _taxGroupRepository.Delete(taxGroup);

        try
        {
            await _taxGroupRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleted TaxGroup with ID {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting TaxGroup with ID {Id}", id);
            throw;
        }
    }

}


