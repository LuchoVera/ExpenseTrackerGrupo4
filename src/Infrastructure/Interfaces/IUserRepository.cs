using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

public interface IUserRepository
{
    User? GetUserById(Guid id);
    void AddUser(User user);
    void UpdateUser(User user);
}

