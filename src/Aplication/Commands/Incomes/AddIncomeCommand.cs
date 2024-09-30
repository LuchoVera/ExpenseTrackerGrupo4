using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;

public class AddIncomeCommand(IIncomeRepository incomeRepository, Income income) : ICommand<Task>
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;
    private readonly Income _income = income;

    public async Task Execute()
    {
       await _incomeRepository.AddAsync(_income);
    }
}
