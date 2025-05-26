using ElectricalBillingRecommendation.Dtos.TaxGroup;

namespace ElectricalBillingRecommendation.Services.Interfaces;

public interface ITaxGroupService
{
    Task<IEnumerable<TaxGroupReadDto>> GetAllTaxGroupAsync(CancellationToken cancellationToken);
    Task<TaxGroupReadDto?> GetByIdTaxGroupAsync(int id, CancellationToken cancellationToken);
    Task<TaxGroupReadDto> CreateTaxGroupAsync(TaxGroupCreateDto taxGroupCreateDto, CancellationToken cancellationToken);
    Task<bool> UpdateTaxGroupAsync(int id, TaxGroupUpdateDto taxGroupUpdateDto, CancellationToken cancellationToken);
    Task<bool> DeleteTaxGroupAsync(int id, CancellationToken cancellationToken);
}

