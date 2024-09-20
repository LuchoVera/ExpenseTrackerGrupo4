using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class AuthenticationService(
    IUserRepository userRepository,
    ITokenValidatorService tokenValidatorService
) : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenValidatorService _tokenValidatorService = tokenValidatorService;

    public async Task<string> Register(User user)
    {
        var existingUser = await _userRepository.GetUserByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists.");
        }

        user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);

        await _userRepository.AddUser(user);

        var token = _tokenValidatorService.GenerateToken(user);

        return token;
    }

    public async Task<string> Login(string email, string password)
    {
        return "";
    }
}
