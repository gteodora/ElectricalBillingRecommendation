using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Services.Interfaces;

public interface ITaxGroupService
{
    Task<IEnumerable<TaxGroupReadDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaxGroupReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaxGroupReadDto> CreateTaxGroupAsync(TaxGroupCreateDto taxGroupCreateDto, CancellationToken cancellationToken);
    Task<bool> UpdateTaxGroupAsync(int id, TaxGroupUpdateDto taxGroupUpdateDto, CancellationToken cancellationToken);
}

