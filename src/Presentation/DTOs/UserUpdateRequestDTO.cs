namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class UserUpdateRequestDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}