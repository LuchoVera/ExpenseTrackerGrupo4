using ExpenseTrackerGrupo4.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task DeleteUserAsync(Guid id);
}

