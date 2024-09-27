namespace ExpenseTrackerGrupo4.src.Utils;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
