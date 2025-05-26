using AutoMapper;
using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Services.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace ElectricalBillingRecommendation.Services;

public class TaxGroupService : ITaxGroupService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<TaxGroupService> _logger;

    public TaxGroupService(AppDbContext context, IMapper mapper, ILogger<TaxGroupService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<TaxGroupReadDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var taxGroups = await _context.TaxGroups
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TaxGroupReadDto>>(taxGroups);
    }

    public async Task<TaxGroupReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var taxGroup = await _context.TaxGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (taxGroup == null)
        {
            _logger.LogWarning("TaxGroup with ID {Id} not found.", id);
            return null;
        }

        return _mapper.Map<TaxGroupReadDto>(taxGroup);
    }

    public async Task<bool> UpdateTaxGroupAsync(int id, TaxGroupUpdateDto taxGroupUpdateDto, CancellationToken cancellationToken)
    {
        var taxGroup = await _context.TaxGroups.FindAsync(new object[] { id }, cancellationToken);

        if (taxGroup == null)
            return false;

        if (taxGroupUpdateDto.Name != null)
            taxGroup.Name = taxGroupUpdateDto.Name;

        if (taxGroupUpdateDto.Vat.HasValue)
            taxGroup.Vat = taxGroupUpdateDto.Vat.Value;

        if (taxGroupUpdateDto.EcoTax.HasValue)
            taxGroup.EcoTax = taxGroupUpdateDto.EcoTax.Value;

        taxGroup.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
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

        await _context.TaxGroups.AddAsync(newTaxGroup, cancellationToken);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
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
}

   
