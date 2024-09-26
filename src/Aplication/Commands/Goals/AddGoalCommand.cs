using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class AddGoalCommand(IGoalRepository goalRepository, Goal goal) : ICommand<Task>
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly Goal _goal = goal;

    public async Task Execute()
    {
       await _goalRepository.AddAsync(_goal);
    }
}
