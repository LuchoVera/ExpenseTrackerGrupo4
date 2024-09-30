using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IGoalNotificationLogRepository
{
    Task<GoalNotificationLog?> GetByGoalIdAsync(Guid goalId, Guid userId);
    Task InsertOrUpdateAsync(GoalNotificationLog log);
}
