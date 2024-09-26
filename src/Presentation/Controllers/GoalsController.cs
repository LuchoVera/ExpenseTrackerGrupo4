using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Route("api/goals")]
[Authorize]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly IMapper _mapper;

    public GoalController(IGoalService goalService, IMapper mapper)
    {
        _goalService = goalService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateUpdateGoalDto dto)
    {
        var userId = GetCurrentUserId();

        if (userId == Guid.Empty) return Forbid(); 

        var goal = _mapper.Map<Goal>(dto);
        goal.UserId = userId; 

        await _goalService.AddAsync(goal);
        return CreatedAtAction(nameof(GetUserGoals), new { id = goal.Id }, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserGoals()
    {
        var userId = GetCurrentUserId();

        if (userId == Guid.Empty) return Forbid(); 

        var goalsWithDetails = await _goalService.GetGoalsWithDetailsAsync(userId);

        return Ok(goalsWithDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] CreateUpdateGoalDto dto)
    {
        var userId = GetCurrentUserId();
        var existingGoal = await _goalService.GetByIdAsync(id, userId);

        if (existingGoal == null) return NotFound();

        existingGoal.GoalAmount = dto.GoalAmount;
        existingGoal.Deadline = dto.Deadline;
        existingGoal.CurrentAmount = dto.CurrentAmount;

        await _goalService.UpdateAsync(existingGoal, userId);
        return Ok(existingGoal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var userId = GetCurrentUserId();
        var existingGoal = await _goalService.GetByIdAsync(id, userId);

        if (existingGoal == null) return NotFound();

        await _goalService.DeleteAsync(id, userId);
        return NoContent();
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }
}
