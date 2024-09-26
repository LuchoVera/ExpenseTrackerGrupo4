using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class UpdateGoalCommand(
    IGoalRepository goalRepository, Goal goal, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly Goal _goal = goal;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        if (_goal.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this goal.");
        }

        await _goalRepository.UpdateAsync(_goal);
    }
}
