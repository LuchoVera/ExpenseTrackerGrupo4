namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class RegisterRequestDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
