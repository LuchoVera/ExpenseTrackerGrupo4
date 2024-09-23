using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Auth;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class AuthenticationService(
    IUserRepository userRepository,
    ITokenValidatorService tokenValidatorService,
    CommandInvoker commandInvoker
) : BaseService(commandInvoker), IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenValidatorService _tokenValidatorService = tokenValidatorService;

    public async Task<string> RegisterAsync(User user)
    {
        var command = new RegisterUserCommand(user, _userRepository, _tokenValidatorService);
        return await CommandInvoker.Execute(command);
    }
    public async Task<string> LoginAsync(string email, string password)
    {
        var command = new LoginUserCommand(email, password, _userRepository, _tokenValidatorService);
        return await CommandInvoker.Execute(command);
    }
}
