using System;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;

public class GetIncomeByIdCommand(
    IIncomeRepository incomeRepository, Guid id, Guid userId
) : ICommand<Task<Income?>>
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;
    private readonly Guid _id = id;
    private readonly Guid _userId = userId;

    public async Task<Income?> Execute()
    {
        var income = await _incomeRepository.GetByIdAsync(_id);
        if (income == null || income.UserId != _userId)
        {
            throw new UnauthorizedAccessException("User does not have access to this income.");
        }
        return income;
    }
}

