using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface ITokenValidatorService
{
    public string GenerateToken(User user);
}
