using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

 public interface IExpenseService : IService<Expense>
{
    Task<List<Expense>> GetUserExpensesCommand(
        Guid userId, DateTime? startDate, DateTime? endDate, string? category
    );
}
