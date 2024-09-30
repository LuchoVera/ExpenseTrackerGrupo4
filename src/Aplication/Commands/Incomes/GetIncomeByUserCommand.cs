using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;

public class GetIncomeByUserCommand(
    IIncomeRepository incomeRepository, Guid userId
) : ICommand<Task<IEnumerable<Income?>>>
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;
    private readonly Guid _userId = userId;

    public async Task<IEnumerable<Income?>> Execute()
    {
        var income = await _incomeRepository.GetIncomesByUserAsync(_userId);

        return income;
        
    }
}

