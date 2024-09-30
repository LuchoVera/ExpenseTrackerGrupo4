using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;

public class UpdateIncomeCommand(
    IIncomeRepository incomeRepository, Income income, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;
    private readonly Income _income = income;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        if (_income.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this income.");
        }

        await _incomeRepository.UpdateAsync(_income);
    }
}
