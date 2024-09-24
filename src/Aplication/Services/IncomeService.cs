using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Application.Interfaces;

namespace ExpenseTrackerGrupo4.src.Application.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public Task AddIncome(Income income)
        {
            return _incomeRepository.AddIncome(income);
        }

        public Task UpdateIncome(Income income)
        {
            return _incomeRepository.UpdateIncome(income);
        }

        public Task DeleteIncome(int id)
        {
            return _incomeRepository.DeleteIncome(id);
        }

        public Task<IEnumerable<Income>> GetIncomesByUser(int userId)
        {
            return _incomeRepository.GetIncomesByUser(userId);
        }

        public Task<Income?> GetIncomeById(int id)
        {
            return _incomeRepository.GetIncomeById(id);
        }
    }
}
