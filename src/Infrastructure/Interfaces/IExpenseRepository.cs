using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetUserExpensesByCategoryAsync(Guid userId, string category);
    Task<List<Expense>> GetUserExpensesByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate);
    Task AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Guid id);
    Task<Expense?> GetByIdAsync(Guid id);
}
