using System.Net;
using System.Net.Mail;
using DotNetEnv;

namespace ExpenseTrackerGrupo4.src.Utils;
public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService()
    {
        _smtpServer = Env.GetString("SMTP_SERVER");
        _smtpPort = Env.GetInt("SMTP_PORT");
        _smtpUser = Env.GetString("SMTP_USER");
        _smtpPass = Env.GetString("SMTP_PASS");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var client = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpUser),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        await client.SendMailAsync(mailMessage);
    }
}
