using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class ExpenseRepository(IDbConnection dbConnection) : IExpenseRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;

    public async Task<List<Expense>> GetUserExpensesAsync(
        Guid userId, DateTime? startDate, DateTime? endDate, string? category
    )
    {
        var query = "SELECT * FROM Expenses WHERE UserId = @UserId";
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);

        if (!string.IsNullOrEmpty(category))
        {
            query += " AND Category = @Category";
            parameters.Add("Category", category);
        }

        if (startDate.HasValue)
        {
            query += " AND Date >= @StartDate";
            parameters.Add("StartDate", startDate.Value);
        }

        if (endDate.HasValue)
        {
            query += " AND Date <= @EndDate";
            parameters.Add("EndDate", endDate.Value);
        }

        var expenses = await _dbConnection.QueryAsync<Expense>(query, parameters);
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
