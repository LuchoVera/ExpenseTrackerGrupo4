using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

 public interface IGoalService : IService<Goal>
{
    Task<List<GoalWithDetails>> GetGoalsWithDetailsAsync(Guid userId);
}
