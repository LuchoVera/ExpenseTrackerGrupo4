using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await _incomeRepository.AddIncome(income);
        }

        public async Task<Income?> GetIncomeByIdAsync(Guid id)
        {
            return await _incomeRepository.GetIncomeById(id);
        }

        public async Task<IEnumerable<Income>> GetIncomesByUserAsync(Guid userId)
        {
            return await _incomeRepository.GetIncomesByUser(userId);
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            await _incomeRepository.UpdateIncome(income);
        }

        public async Task DeleteIncomeAsync(Guid id)
        {
            await _incomeRepository.DeleteIncome(id);
        }
    }
}
