using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class ExpenseService (
    IExpenseRepository expenseRepository, CommandInvoker commandInvoker
) : BaseService(commandInvoker), IExpenseService
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;

    public async Task AddAsync(Expense expense)
    {
        var command = new AddExpenseCommand(_expenseRepository, expense);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var command = new DeleteExpenseCommand(_expenseRepository, id, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Expense?> GetByIdAsync(Guid id, Guid userId)
    {
        var command = new GetExpenseByIdCommand(_expenseRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<Expense?> GetExpenseByIdAsync(Guid id, Guid userId)
    {
        var command = new GetExpenseByIdCommand(_expenseRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<List<Expense>> GetUserExpensesCommand(
        Guid userId, DateTime? startDate, DateTime? endDate, string? category
    )
    {
        var command = new GetUserExpensesCommand(_expenseRepository, userId, startDate, endDate, category);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Expense expense, Guid userId)
    {
        var command = new UpdateExpenseCommand(_expenseRepository, expense, userId);
        await CommandInvoker.Execute(command);
    }
}

