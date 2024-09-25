using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetUserExpensesAsync(
        Guid userId, DateTime? startDate, DateTime? endDate, string? category
    );
    Task AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Guid id);
    Task<Expense?> GetByIdAsync(Guid id);
}
