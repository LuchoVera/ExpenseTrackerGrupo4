using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly IDbConnection _connection;

        public IncomeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddIncome(Income income)
        {
            var query = "INSERT INTO Incomes (Id, UserId, Amount, Source, Date, CreatedAt) VALUES (@Id, @UserId, @Amount, @Source, @Date, @CreatedAt)";
            await _connection.ExecuteAsync(query, income);
        }

        public async Task<Income?> GetIncomeById(Guid id)
        {
            var query = "SELECT * FROM Incomes WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Income>(query, new { Id = id });
        }

        public async Task<IEnumerable<Income>> GetIncomesByUser(Guid userId)
        {
            var query = "SELECT * FROM Incomes WHERE UserId = @UserId";
            return await _connection.QueryAsync<Income>(query, new { UserId = userId });
        }

        public async Task UpdateIncome(Income income)
        {
            var query = "UPDATE Incomes SET Amount = @Amount, Source = @Source, Date = @Date WHERE Id = @Id";
            await _connection.ExecuteAsync(query, income);
        }

        public async Task DeleteIncome(Guid id)
        {
            var query = "DELETE FROM Incomes WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
