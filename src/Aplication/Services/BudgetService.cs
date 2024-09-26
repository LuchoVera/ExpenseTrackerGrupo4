using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class BudgetService (
    IBudgetRepository budgetRepository, CommandInvoker commandInvoker
) : BaseService(commandInvoker), IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task AddAsync(Budget budget)
    {
        var command = new AddBudgetCommand(_budgetRepository, budget);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var command = new DeleteBudgetCommand(_budgetRepository, id, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Budget?> GetByIdAsync(Guid id, Guid userId)
    {
        var command = new GetBudgetByIdCommand(_budgetRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<List<BudgetWithExpenses>> GetBudgetsAsync(Guid userId)
    {
        var command = new GetUserBudgetsCommand(_budgetRepository, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Budget budget, Guid userId)
    {
        var command = new UpdateBudgetCommand(_budgetRepository, budget, userId);
        await CommandInvoker.Execute(command);
    }

}

