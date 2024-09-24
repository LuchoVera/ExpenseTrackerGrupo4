using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Application.Interfaces
{
    public interface IIncomeService
    {
        Task AddIncome(Income income);
        Task UpdateIncome(Income income);
        Task DeleteIncome(int id);
        Task<IEnumerable<Income>> GetIncomesByUser(int userId);
        Task<Income?> GetIncomeById(int id);
    }
}
