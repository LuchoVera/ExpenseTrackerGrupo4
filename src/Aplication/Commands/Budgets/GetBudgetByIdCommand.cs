using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetBudgetByIdCommand(
    IBudgetRepository budgetRepository, Guid id, Guid userId
) : ICommand<Task<Budget?>>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly Guid _id = id;
    private readonly Guid _userId = userId;

    public async Task<Budget?> Execute()
    {
        var budget = await _budgetRepository.GetByIdAsync(_id);
        if (budget == null || budget.UserId != _userId)
        {
            throw new UnauthorizedAccessException("User does not have access to this budget.");
        }
        return budget;
    }
}

