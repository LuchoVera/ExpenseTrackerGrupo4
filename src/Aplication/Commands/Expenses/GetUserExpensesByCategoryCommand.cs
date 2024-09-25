using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetUserExpensesByCategoryCommand(
    IExpenseRepository expenseRepository, Guid userId, string category
) : ICommand<Task<List<Expense>>>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Guid _userId = userId;
    private readonly string _category = category;

    public async Task<List<Expense>> Execute()
    {
        return await _expenseRepository.GetUserExpensesByCategoryAsync(_userId, _category);
    }
}

