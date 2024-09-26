using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class Income : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required decimal Amount { get; set; }
    public required string Source { get; set; }
    public required DateTime Date { get; set; }
    public required DateTime CreatedAt { get; init; } = DateTime.Now;

    public Income(Guid id, Guid userId, decimal amount, string source, DateTime date)
    {
        Id = id;
        UserId = userId;
        Amount = amount;
        Source = source;
        Date = date;
    }

    public Income() {}
}