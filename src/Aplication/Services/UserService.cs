using System;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task UpdateProfile(User user)
    {
        return _userRepository.UpdateUser(user);
    }


    public Task<User?> GetUserById(Guid userId)
    {
        return _userRepository.GetUserById(userId);
    }

    private string HashPassword(string password)
    {
        return password;
    }
}

