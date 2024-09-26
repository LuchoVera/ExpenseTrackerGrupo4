using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class AddExpenseCommand(IExpenseRepository expenseRepository, Expense expense) : ICommand<Task>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Expense _expense = expense;

    public async Task Execute()
    {
       await _expenseRepository.AddAsync(_expense);
    }
}

