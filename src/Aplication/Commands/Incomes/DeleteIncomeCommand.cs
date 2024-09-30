using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;

public class DeleteIncomeCommand(
    IIncomeRepository incomeRepository, Guid incomeId, Guid authenticatedUserId
) : ICommand<Task>
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;
    private readonly Guid _incomeId = incomeId;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(_incomeId);
        if (existingIncome == null || existingIncome.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("User does not have access to this income.");
        }
        await _incomeRepository.DeleteAsync(_incomeId);
    }
}

