using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface IUserService
{
    Task UpdateProfile(User user);
    // Task ResetPassword(string email);
    Task<User?> GetUserById(Guid userId);
}

