using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Dtos.TaxGroup;
using ElectricalBillingRecommendation.Services.Interfaces;

namespace ElectricalBillingRecommendation.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TaxGroupsController : ControllerBase
    {
        private readonly ITaxGroupService _taxGroupService;

        private readonly AppDbContext _context;  //

        public TaxGroupsController(ITaxGroupService taxGroupService, AppDbContext context)
        {
            _taxGroupService = taxGroupService;
            
            _context = context;
        }

        // GET: api/TaxGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxGroupReadDto>>> GetTaxGroups(CancellationToken cancellationToken)
        {
            try
            {
                var taxGroupReadDtos = await _taxGroupService.GetAllAsync(cancellationToken);
                return Ok(taxGroupReadDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving tax groups.");
            }  
        }

        // GET: api/TaxGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxGroupReadDto>> GetTaxGroup(int id, CancellationToken cancellationToken)
        {
            try
            {
                var taxGroupReadDto = await _taxGroupService.GetByIdAsync(id, cancellationToken);
                if (taxGroupReadDto == null)
                    return NotFound();

                return Ok(taxGroupReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the TaxGroup.");
            }
        }

        // PUT: api/TaxGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxGroup(int id, 
            [FromBody] TaxGroupUpdateDto taxGroupUpdateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool updated;

            try
            {
                updated = await _taxGroupService.UpdateTaxGroupAsync(id, taxGroupUpdateDto, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict("TaxGroup was modified by another user.");
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

            if (!updated)
                return NotFound();

            return NoContent(); // 204 No Content je standardni odgovor za update bez vraćanja tijela
        }

        [HttpPost]
        public async Task<ActionResult<TaxGroupReadDto>> PostTaxGroup(
             [FromBody] TaxGroupCreateDto taxGroupCreateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _taxGroupService.CreateTaxGroupAsync(taxGroupCreateDto, cancellationToken);
                return CreatedAtAction(nameof(GetTaxGroup), new { id = created.Id }, created);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "A database error occurred.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // DELETE: api/TaxGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxGroup(int id)
        {
            var taxGroup = await _context.TaxGroups.FindAsync(id);
            if (taxGroup == null)
            {
                return NotFound();
            }

            _context.TaxGroups.Remove(taxGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

