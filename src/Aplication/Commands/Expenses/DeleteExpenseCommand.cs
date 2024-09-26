using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class DeleteExpenseCommand(
    IExpenseRepository expenseRepository, Guid expenseId, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Guid _expenseId = expenseId;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        var existingExpense = await _expenseRepository.GetByIdAsync(_expenseId);
        if (existingExpense == null || existingExpense.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this expense.");
        }
        await _expenseRepository.DeleteAsync(_expenseId);
    }
}

