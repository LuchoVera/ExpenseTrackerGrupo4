using System;

namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class CreateUpdateExpenseDto
{
    public decimal? Amount { get; set; } 
    public string? Description { get; set; } 
    public string? Category { get; set; } 
    public DateTime? Date { get; set; } 
}

