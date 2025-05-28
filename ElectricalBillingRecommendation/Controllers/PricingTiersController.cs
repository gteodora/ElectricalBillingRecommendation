
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectricalBillingRecommendation.Models;
using ElectricalBillingRecommendation.Services.Interfaces;
using ElectricalBillingRecommendation.Services;
using System.Threading;
using ElectricalBillingRecommendation.Dtos.PricingTier;

namespace ElectricalBillingRecommendation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingTiersController : ControllerBase
    {
        private readonly IPricingTierService _service;

        public PricingTiersController(IPricingTierService service)
        {
            _service = service;
        }

        // GET: api/PricingTiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PricingTierReadDto>>> GetPricingTiersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var pricingTierReadDtos = await _service.GetAllAsync(cancellationToken);
                return Ok(pricingTierReadDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving pricing tiers.");
            }
        }

        // GET: api/PricingTiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PricingTierReadDto>> GetPricingTier(int id, CancellationToken cancellationToken)
        {
            try
            {
                var pricingTierReadDto = await _service.GetByIdAsync(id, cancellationToken);
                if (pricingTierReadDto == null)
                    return NotFound();

                return Ok(pricingTierReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the PricingTier.");
            }
        }

        // PUT: api/PricingTiers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPricingTier(int id, PricingTierUpdateDto pricingTierUpdateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isUpdated;

            try
            {
                isUpdated = await _service.UpdateAsync(id, pricingTierUpdateDto, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict("PricingTier was modified by another user.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database update error.");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(499, "Request was canceled.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error.");
            }

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // POST: api/PricingTiers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PricingTierReadDto>> PostPricingTier(PricingTierCreateDto pricingTierCreateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(pricingTierCreateDto, cancellationToken);
                return CreatedAtAction(nameof(GetPricingTier), new { id = created.Id }, created);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "A database error occurred.");
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // DELETE: api/PricingTiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePricingTier(int id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await _service.DeleteAsync(id, cancellationToken);
                if (!isDeleted)
                    return NotFound();

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
