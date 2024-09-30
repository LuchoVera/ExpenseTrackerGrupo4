using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Incomes;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services
{
    public class IncomeService(
        IIncomeRepository incomeRepository, 
        CommandInvoker commandInvoker
    ) : BaseService(commandInvoker), IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository = incomeRepository;

        public async Task AddAsync(Income income)
        {
            var command = new AddIncomeCommand(_incomeRepository, income);
            await CommandInvoker.Execute(command);
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var command = new DeleteIncomeCommand(_incomeRepository, id, userId);
            await CommandInvoker.Execute(command);
        }

        public async Task<Income?> GetByIdAsync(Guid id, Guid userId)
        {
            var command = new GetIncomeByIdCommand(_incomeRepository, id, userId);
            return await CommandInvoker.Execute(command);
        }

        public async Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId)
        {
            var command = new GetIncomeByUserCommand(_incomeRepository, userId);
            return await CommandInvoker.Execute(command);
        }

        public async Task UpdateAsync(Income income, Guid userId)
        {
            var command = new UpdateIncomeCommand(_incomeRepository, income, userId);
            await CommandInvoker.Execute(command);
        }
    }
}
