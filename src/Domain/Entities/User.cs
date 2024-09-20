using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}

