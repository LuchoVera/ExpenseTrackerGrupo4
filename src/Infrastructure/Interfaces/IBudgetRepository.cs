using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<List<BudgetWithExpenses>> GetBudgetsAsync(Guid userId);
}
