using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task AddAsync(Income entity)
        {
            await _incomeRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            await _incomeRepository.DeleteAsync(id);
        }

        public async Task<Income?> GetByIdAsync(Guid id, Guid userId)
        {
            var income = await _incomeRepository.GetByIdAsync(id);
            return income;
        }

        public async Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId)
        {
           var income = await _incomeRepository.GetIncomesByUserAsync(userId);
           return income;
        }

        public async Task UpdateAsync(Income entity, Guid userId)
        {
            await _incomeRepository.UpdateAsync(entity);
        }
    }
}
