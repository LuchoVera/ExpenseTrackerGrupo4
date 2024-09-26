using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetUserBudgetsCommand(
    IBudgetRepository budgetRepository, 
    Guid userId
) : ICommand<Task<List<BudgetWithExpenses>>>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly Guid _userId = userId;

    public async Task<List<BudgetWithExpenses>> Execute()
    {
        return await _budgetRepository.GetBudgetsAsync(_userId);
    }
}


