using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetGoalByIdCommand(
    IGoalRepository goalRepository, Guid id, Guid userId
) : ICommand<Task<Goal?>>
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly Guid _id = id;
    private readonly Guid _userId = userId;

    public async Task<Goal?> Execute()
    {
        var goal = await _goalRepository.GetByIdAsync(_id);
        if (goal == null || goal.UserId != _userId)
        {
            throw new UnauthorizedAccessException("User does not have access to this goal.");
        }
        return goal;
    }
}
