using ElectricalBillingRecommendation.Models;

namespace ElectricalBillingRecommendation.Services.Interfaces;

public interface IRecommendationService
{
   Task<Recommendation> GetAllAsync(int taxGroupId, int consumption, CancellationToken cancellationToken);
}

