namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class PasswordResetConfirmDto
{
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}
