using System;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetExpenseByIdCommand(
    IExpenseRepository expenseRepository, Guid id, Guid userId
) : ICommand<Task<Expense?>>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Guid _id = id;
    private readonly Guid _userId = userId;

    public async Task<Expense?> Execute()
    {
        var expense = await _expenseRepository.GetByIdAsync(_id);
        if (expense == null || expense.UserId != _userId)
        {
            throw new UnauthorizedAccessException("User does not have access to this expense.");
        }
        return expense;
    }
}

