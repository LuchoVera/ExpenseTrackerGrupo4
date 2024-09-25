namespace ExpenseTrackerGrupo4.src.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<T?> GetById(Guid id);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
