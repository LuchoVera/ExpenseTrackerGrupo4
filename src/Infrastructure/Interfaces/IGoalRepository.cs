using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IGoalRepository : IRepository<Goal>
{
    Task<List<GoalWithDetails>> GetGoalsWithDetailsAsync(Guid userId);
}
