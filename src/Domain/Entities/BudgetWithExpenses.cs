namespace ExpenseTrackerGrupo4.src.Domain.Entities;
public class BudgetWithExpenses
{
    public Guid BudgetId { get; set; }
    public Guid UserId { get; set; }
    public DateTime Month { get; set; }
    public decimal BudgetAmount { get; set; }
    public Guid? ExpenseId { get; set; } // Puede ser nulo si no hay gastos
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? CreatedAt { get; set; }
}
