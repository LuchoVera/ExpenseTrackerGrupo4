using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces
{
    public interface IIncomeService : IService<Income>
    {
        Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId);

    }

}
