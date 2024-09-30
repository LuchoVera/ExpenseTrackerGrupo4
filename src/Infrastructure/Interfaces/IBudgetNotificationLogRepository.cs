using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IBudgetNotificationLogRepository
{
    Task<BudgetNotificationLog?> GetByBudgetIdAsync(Guid budgetId, Guid userId);
    Task InsertOrUpdateAsync(BudgetNotificationLog log);
}
