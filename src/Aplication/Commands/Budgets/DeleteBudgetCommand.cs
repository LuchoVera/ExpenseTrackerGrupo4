using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class DeleteBudgetCommand(
    IBudgetRepository budgetRepository, Guid budgetId, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly Guid _budgetId = budgetId;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        var existingBudget = await _budgetRepository.GetByIdAsync(_budgetId);
        if (existingBudget == null || existingBudget.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this budget.");
        }
        await _budgetRepository.DeleteAsync(_budgetId);
    }
}

