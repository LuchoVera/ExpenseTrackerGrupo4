using System;
using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

public interface IService<T> where T : IEntity
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
    Task<T?> GetByIdAsync(Guid id, Guid userId);
}
