using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserById(Guid id);
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task<User?> GetUserByEmail(string email);
}

