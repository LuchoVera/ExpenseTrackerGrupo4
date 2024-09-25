using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class ExpenseRepository(IDbConnection dbConnection) : IExpenseRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;

    public async Task<List<Expense>> GetUserExpensesByCategoryAsync(Guid userId, string category)
    {
        var query = "SELECT * FROM Expenses WHERE UserId = @UserId AND Category = @Category";
        var expenses = await _dbConnection.QueryAsync<Expense>(query, new 
            { UserId = userId, Category = category }
        );
        return expenses.ToList();
    }

    public async Task<List<Expense>> GetUserExpensesByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate)
    {
        var query = "SELECT * FROM Expenses " +
                    "WHERE UserId = @UserId AND Date >= @StartDate AND Date <= @EndDate";
        var expenses = await _dbConnection.QueryAsync<Expense>(query, new
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate
            });
        return expenses.ToList();
    }

    public async Task AddAsync(Expense expense)
    {
        var query = "INSERT INTO Expenses (Id, UserId, Amount, Description, Category, Date, CreatedAt) " +
                    "VALUES (@Id, @UserId, @Amount, @Description, @Category, @Date, @CreatedAt)";
        await _dbConnection.ExecuteAsync(query, expense);
    }

    public async Task UpdateAsync(Expense expense)
    {
        var query = "UPDATE Expenses " + 
                    "SET Amount = @Amount, Description = @Description, Category = @Category, Date = @Date " +
                    "WHERE Id = @Id AND UserId = @UserId";
        await _dbConnection.ExecuteAsync(query, expense);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "DELETE FROM Expenses WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Expense?> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM Expenses WHERE Id = @Id";
        var expense = await _dbConnection.QuerySingleOrDefaultAsync<Expense>(query, new { Id = id });
        return expense;
    }
}
