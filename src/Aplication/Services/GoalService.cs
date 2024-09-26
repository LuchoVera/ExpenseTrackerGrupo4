using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class GoalService (
    IGoalRepository goalRepository, CommandInvoker commandInvoker
) : BaseService(commandInvoker), IGoalService
{
    private readonly IGoalRepository _goalRepository = goalRepository;

    public async Task AddAsync(Goal goal)
    {
        var command = new AddGoalCommand(_goalRepository, goal);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var command = new DeleteGoalCommand(_goalRepository, id, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Goal?> GetByIdAsync(Guid id, Guid userId)
    {
        var command = new GetGoalByIdCommand(_goalRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<List<GoalWithDetails>> GetGoalsWithDetailsAsync(Guid userId)
    {
        var command = new GetUserGoalsCommand(_goalRepository, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Goal goal, Guid userId)
    {
        var command = new UpdateGoalCommand(_goalRepository, goal, userId);
        await CommandInvoker.Execute(command);
    }

}
