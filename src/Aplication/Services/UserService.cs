using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }
}
