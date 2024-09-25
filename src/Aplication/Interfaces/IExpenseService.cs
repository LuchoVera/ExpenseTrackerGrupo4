using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

 public interface IExpenseService : IService<Expense>
{
    Task<List<Expense>> GetUserExpensesByCategoryAsync(Guid userId, string category);
    Task<List<Expense>> GetUserExpensesByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate);
}
