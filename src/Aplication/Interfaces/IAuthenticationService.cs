using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user);
    Task<string> LoginAsync(string email, string password);
}
