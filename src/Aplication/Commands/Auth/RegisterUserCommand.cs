using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Auth;

public class RegisterUserCommand(
    User user,
    IUserRepository userRepository,
    ITokenValidatorService tokenValidatorService
) : ICommand<Task<string>>
{
    public User User { get; } = user;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenValidatorService _tokenValidatorService = tokenValidatorService;

    public async Task<string> Execute()
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(User.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists.");
        }

        User.PasswordHash = PasswordHasher.HashPassword(User.PasswordHash);
        await _userRepository.Add(User);

        var token = _tokenValidatorService.GenerateToken(User);
        return token;
    }
}
