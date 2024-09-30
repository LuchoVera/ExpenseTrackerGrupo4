using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] Income income)
        {
            if (income == null)
            {
                return BadRequest("Income data is required.");
            }

            var currentUser = UserIdClaimer.GetCurrentUserId(User);

            if (currentUser == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            income.UserId = currentUser;

            await _incomeService.AddIncomeAsync(income);
            return CreatedAtAction(nameof(GetIncomeById), new { id = income.Id }, income);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            if (currentUser == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var incomes = await _incomeService.GetIncomesByUserIdAsync(currentUser);
            
            if (incomes == null || !incomes.Any())
            {
                return Ok(new List<Income>());
            }

            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncomeById(Guid id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            var currentUser = UserIdClaimer.GetCurrentUserId(User);
    
            if (income.UserId != currentUser)
            {
                return Forbid();
            }

            return Ok(income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(Guid id, [FromBody] Income updatedIncome)
        {
            if (updatedIncome == null || updatedIncome.Id != id)
            {
                return BadRequest("Income data is invalid.");
            }

            var existingIncome = await _incomeService.GetIncomeByIdAsync(id);
            if (existingIncome == null)
            {
                return NotFound();
            }

            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            
            if (existingIncome.UserId != currentUser)
            {
                return Forbid();
            }

            await _incomeService.UpdateIncomeAsync(updatedIncome);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            
            if (income.UserId != currentUser)
            {
                return Forbid();
            }

            await _incomeService.DeleteIncomeAsync(id);
            return NoContent();
        }
    }
}
