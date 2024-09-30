namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class BudgetNotificationLog
{
    public Guid UserId { get; set; }
    public Guid BudgetId { get; set; }
    public bool Notified80 { get; set; }
    public bool Notified90 { get; set; }
    public bool Notified100 { get; set; }
}
