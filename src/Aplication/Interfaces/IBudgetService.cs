using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

 public interface IBudgetService : IService<Budget>
{
    Task<List<BudgetWithExpenses>> GetBudgetsAsync(Guid userId);
    Task CheckBudgetNotificationsAsync(Guid userId);
}
