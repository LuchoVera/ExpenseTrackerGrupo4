using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetUserGoalsCommand(
    IGoalRepository goalRepository, 
    Guid userId
) : ICommand<Task<List<GoalWithDetails>>>
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly Guid _userId = userId;

    public async Task<List<GoalWithDetails>> Execute()
    {
        return await _goalRepository.GetGoalsWithDetailsAsync(_userId);
    }
}
