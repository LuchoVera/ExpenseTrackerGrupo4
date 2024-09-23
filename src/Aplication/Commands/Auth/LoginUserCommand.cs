using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Auth;

public class LoginUserCommand(
    string email,
    string password,
    IUserRepository userRepository,
    ITokenValidatorService tokenValidatorService
) : ICommand<Task<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenValidatorService _tokenValidatorService = tokenValidatorService;

    public async Task<string> Execute()
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, password))
        {
            throw new Exception("Invalid email or password.");
        }

        var token = _tokenValidatorService.GenerateToken(user);
        return token;
    }
}
