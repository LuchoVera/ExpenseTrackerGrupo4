using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class Goal : IEntity
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required decimal GoalAmount { get; set; }
    public required DateTime Deadline { get; set; }
    public required decimal CurrentAmount { get; set; }
    public required DateTime CreatedAt { get; init; } = DateTime.Now;
}

