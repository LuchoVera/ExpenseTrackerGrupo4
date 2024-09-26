using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task AddIncomeAsync(Income income)
        {
            await _incomeRepository.AddAsync(income);
        }

        public async Task<Income?> GetIncomeByIdAsync(Guid id)
        {
            return await _incomeRepository.GetByIdAsync(id);
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            await _incomeRepository.UpdateAsync(income);
        }

        public async Task DeleteIncomeAsync(Guid id)
        {
            await _incomeRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Income>> GetIncomesByUserIdAsync(Guid userId)
        {
            return _incomeRepository.GetIncomesByUser(userId);
        }
    }
}
