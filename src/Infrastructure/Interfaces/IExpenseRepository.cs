using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<List<Expense>> GetUserExpensesAsync(
        Guid userId, DateTime? startDate, DateTime? endDate, string? category
    );
}
