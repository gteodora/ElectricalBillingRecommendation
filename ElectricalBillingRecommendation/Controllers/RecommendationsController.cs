using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectricalBillingRecommendation.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        // GET: api/<RecommendationsController>
        [HttpGet]
        public async Task<ActionResult<Recommendation>> GetAsync([FromQuery] int taxGroupId, int averageMonthlyConsumption, CancellationToken cancellationToken)
        {
            try
            {
                var recommendation = await _recommendationService.GetAllAsync(taxGroupId, averageMonthlyConsumption, cancellationToken);

                if (recommendation == null)
                {
                    return NotFound("No suitable recommendation found.");
                }

                return Ok(recommendation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving planss.");
            }
        } 
    }

