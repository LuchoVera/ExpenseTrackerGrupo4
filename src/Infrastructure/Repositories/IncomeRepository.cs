using Dapper;
using System.Data;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly IDbConnection _dbConnection;

        public IncomeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Add(Income income)
        {
            var query = "INSERT INTO Incomes (id, userid, amount, source, date, createdat) " +
                        "VALUES (@Id, @UserId, @Amount, @Source, @Date, @CreatedAt)";
            await _dbConnection.ExecuteAsync(query, income);
        }

        public async Task<Income?> GetById(Guid id)
        {
            var query = "SELECT * FROM Incomes WHERE id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Income>(query, new { Id = id });
        }

        public async Task Update(Income income)
        {
            var query = "UPDATE Incomes SET userid = @UserId, amount = @Amount, source = @Source, " +
                        "date = @Date, createdat = @CreatedAt WHERE id = @Id";
            await _dbConnection.ExecuteAsync(query, income);
        }

        public async Task Delete(Guid id)
        {
            var query = "DELETE FROM Incomes WHERE id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Income>> GetIncomesByUser(Guid userId)
        {
            var query = "SELECT * FROM Incomes WHERE userid = @UserId";
            return await _dbConnection.QueryAsync<Income>(query, new { UserId = userId });
        }
    }
}
