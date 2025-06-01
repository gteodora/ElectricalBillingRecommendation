using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectricalBillingRecommendation.Services.Interfaces;
using ElectricalBillingRecommendation.Dtos.Plan;
using Microsoft.AspNetCore.Authorization;

namespace ElectricalBillingRecommendation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        // GET: api/Plans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanReadDto>>> GetPlans(CancellationToken cancellationToken)
        {
            try
            {
                var planReadDtos = await _planService.GetAllAsync(cancellationToken);
                return Ok(planReadDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving planss.");
            }
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanReadDto>> GetPlan(int id, CancellationToken cancellationToken)
        {
            try
            {
                var planReadDto = await _planService.GetByIdAsync(id, cancellationToken);
                if (planReadDto == null)
                    return NotFound();

                return Ok(planReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the Plan.");
            }
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, 
            [FromBody] PlanUpdateDto planUpdateDto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isUpdated;

            try
            {
                isUpdated = await _planService.UpdateAsync(id, planUpdateDto, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict("Plan was modified by another user.");
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

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlanReadDto>> PostPlan(
             [FromBody] PlanCreateDto planCreateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _planService.CreateAsync(planCreateDto, cancellationToken);
                return CreatedAtAction(nameof(GetPlan), new { id = created.Id }, created);
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

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await _planService.DeleteAsync(id, cancellationToken);
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
