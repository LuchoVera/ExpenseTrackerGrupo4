using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class UpdateExpenseCommand(
    IExpenseRepository expenseRepository, Expense expense, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Expense _expense = expense;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        if (_expense.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this expense.");
        }

        await _expenseRepository.UpdateAsync(_expense);
    }
}


