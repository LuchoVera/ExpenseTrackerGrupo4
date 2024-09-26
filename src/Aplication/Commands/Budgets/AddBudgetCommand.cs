using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class AddBudgetCommand(IBudgetRepository budgetRepository, Budget budget) : ICommand<Task>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly Budget _budget = budget;

    public async Task Execute()
    {
       await _budgetRepository.AddAsync(_budget);
    }
}

