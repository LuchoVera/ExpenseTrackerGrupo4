using System;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task UpdateProfileAsync(User user)
    {
        return _userRepository.UpdateUserAsync(user);
    }

    public Task<User?> GetUserByIdAsync(Guid userId)
    {
        return _userRepository.GetUserByIdAsync(userId);
    }
}

