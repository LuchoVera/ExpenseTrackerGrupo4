namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class GoalWithDetails
{
    public Guid GoalId { get; set; }
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
}
