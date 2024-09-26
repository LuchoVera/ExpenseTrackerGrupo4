using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class DeleteGoalCommand(
    IGoalRepository goalRepository, Guid goalId, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly Guid _goalId = goalId;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        var existingGoal = await _goalRepository.GetByIdAsync(_goalId);
        if (existingGoal == null || existingGoal.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this goal.");
        }
        await _goalRepository.DeleteAsync(_goalId);
    }
}
