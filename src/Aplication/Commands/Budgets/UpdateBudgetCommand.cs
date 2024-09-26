using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class UpdateBudgetCommand(
    IBudgetRepository budgetRepository, Budget budget, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly Budget _budget = budget;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        if (_budget.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this budget.");
        }

        await _budgetRepository.UpdateAsync(_budget);
    }
}


