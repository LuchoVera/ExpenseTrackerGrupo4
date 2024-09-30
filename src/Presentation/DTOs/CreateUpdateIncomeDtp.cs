namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class CreateUpdateIncomeDto
{
    public decimal Amount { get; set; }
    public string? Source { get; set; }
    public DateTime Date { get; set; }
}