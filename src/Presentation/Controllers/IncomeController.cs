using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IMapper _mapper;

        public IncomeController(IIncomeService incomeService, IMapper mapper)
        {
            _incomeService = incomeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] CreateUpdateIncomeDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Income data is required.");
            }

            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var income = _mapper.Map<Income>(dto);
            income.UserId = userId;

            await _incomeService.AddAsync(income);
            return CreatedAtAction(nameof(GetIncomeById), new { id = income.Id }, dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var incomes = await _incomeService.GetIncomesByUserId(userId);
            
            if (incomes == null || !incomes.Any())
            {
                return Ok(new List<Income>());
            }

            return Ok(incomes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncomeById(Guid id)
        {
            var userId = GetUserId();
            var income = await _incomeService.GetByIdAsync(id, userId);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(Guid id, [FromBody] CreateUpdateIncomeDto dto)
        {
            var userId = GetUserId();
            var existingIncome = await _incomeService.GetByIdAsync(id, userId);

            if (existingIncome == null)
            {
                return NotFound();
            }

            existingIncome.Amount = dto.Amount;
            existingIncome.Source = dto.Source ?? existingIncome.Source;
            existingIncome.Date = dto.Date;

            await _incomeService.UpdateAsync(existingIncome, userId);
            return Ok(existingIncome);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var userId = GetUserId();
            var income = await _incomeService.GetByIdAsync(id, userId);
            if (income == null)
            {
                return NotFound();
            }

            await _incomeService.DeleteAsync(id, userId);
            return Ok();
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }
    }
}
