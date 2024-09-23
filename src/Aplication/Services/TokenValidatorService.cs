using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Token;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class TokenValidatorService(CommandInvoker commandInvoker
) : BaseService(commandInvoker), ITokenValidatorService
{
    public string GenerateToken(User user)
    {
        var command = new GenerateTokenCommand(user);
        return CommandInvoker.Execute(command);
    }

    public bool ValidateToken(string token)
    {
        return false;
    }
}
