using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces
{
    public interface IIncomeService
    {
        Task AddIncomeAsync(Income income);
        Task<Income?> GetIncomeByIdAsync(Guid id);
        Task<IEnumerable<Income>> GetIncomesByUserIdAsync(Guid userId);
        Task UpdateIncomeAsync(Income income);
        Task DeleteIncomeAsync(Guid id);
    }

}
