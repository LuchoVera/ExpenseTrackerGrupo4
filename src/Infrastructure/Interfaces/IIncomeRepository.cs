using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces
{
    public interface IIncomeRepository
    {
        Task AddIncome(Income income);
        Task<Income?> GetIncomeById(Guid id);
        Task<IEnumerable<Income>> GetIncomesByUser(Guid userId);
        Task UpdateIncome(Income income);
        Task DeleteIncome(Guid id);
    }

}
