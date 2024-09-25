using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetExpensesByDateRangeCommand(
    IExpenseRepository expenseRepository, 
    Guid userId, 
    DateTime? startDate, 
    DateTime? endDate
) : ICommand<Task<List<Expense>>>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Guid _userId = userId;
    private readonly DateTime? _startDate = startDate;
    private readonly DateTime? _endDate = endDate;

    public async Task<List<Expense>> Execute()
    {
        return await _expenseRepository.GetUserExpensesByDateRangeAsync(
            _userId, _startDate ?? DateTime.MinValue, _endDate ?? DateTime.Now
        );
    }
}
