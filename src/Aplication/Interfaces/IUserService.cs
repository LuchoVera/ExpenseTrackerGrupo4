using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface IUserService
{
    Task UpdateProfileAsync(User user);
    // Task ResetPasswordAsync(string email);
    Task<User?> GetUserByIdAsync(Guid userId);
}

