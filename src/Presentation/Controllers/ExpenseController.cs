using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Route("api/expenses")]
[Authorize]
public class ExpenseController(IExpenseService expenseService, IMapper mapper) : ControllerBase
{
    private readonly IExpenseService _expenseService = expenseService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateExpense([FromBody] CreateUpdateExpenseDto dto)
    {
        var userId = GetCurrentUserId();

        if(userId == Guid.Empty) return Forbid(); 

        var expense = _mapper.Map<Expense>(dto);
        expense.UserId = userId; 

        await _expenseService.AddAsync(expense);
        return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, dto);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserExpenses(
        [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? category
    )
    {
        var userId = GetCurrentUserId();

        if(userId == Guid.Empty) return Forbid(); 

        var expenses = await _expenseService.GetUserExpensesCommand(userId, startDate, endDate, category);

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetExpenseById(Guid id)
    {
        var userId = GetCurrentUserId();
        try
        {
            var expense = await _expenseService.GetByIdAsync(id, userId);
            if (expense == null) return NotFound();
            return Ok(expense);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); 
        }

    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] CreateUpdateExpenseDto dto)
    {
        var userId = GetCurrentUserId();
        var existingExpense = await _expenseService.GetByIdAsync(id, userId);

        if (existingExpense == null) return NotFound();

        existingExpense.Amount = dto.Amount ?? existingExpense.Amount;
        existingExpense.Description = dto.Description ?? existingExpense.Description;
        existingExpense.Category = dto.Category ?? existingExpense.Category;
        existingExpense.Date = dto.Date ?? existingExpense.Date;

        try
        {
            await _expenseService.UpdateAsync(existingExpense, userId);
            return Ok(existingExpense);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); 
        }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var userId = GetCurrentUserId();
        var existingExpense = await _expenseService.GetByIdAsync(id, userId);

        if (existingExpense == null) return NotFound();

        try
        {
            await _expenseService.DeleteAsync(id, userId);
            return Ok(); 
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); 
        }
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }
}
