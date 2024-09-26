using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IDbConnection _dbConnection;

    public BudgetRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task AddAsync(Budget budget)
    {
        var existingBudgetQuery = @"
            SELECT COUNT(1) 
            FROM Budgets 
            WHERE UserId = @UserId 
            AND DATE_TRUNC('month', Month) = DATE_TRUNC('month', @Month)";
        
        var existingBudgetCount = await _dbConnection.ExecuteScalarAsync<int>(existingBudgetQuery, new { budget.UserId, budget.Month });
        
        if (existingBudgetCount > 0)
        {
            throw new InvalidOperationException("A budget already exists for this month and user.");
        }

        var query = @"
            INSERT INTO Budgets (Id, UserId, Month, BudgetAmount) 
            VALUES (@Id, @UserId, @Month, @BudgetAmount)";
        
        await _dbConnection.ExecuteAsync(query, budget);
    }


    public async Task UpdateAsync(Budget budget)
    {
        var query = @"
            UPDATE Budgets 
            SET BudgetAmount = @BudgetAmount, Month = @Month
            WHERE Id = @Id AND UserId = @UserId";
        await _dbConnection.ExecuteAsync(query, budget);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "DELETE FROM Budgets WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Budget?> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM Budgets WHERE Id = @Id";
        var budget = await _dbConnection.QuerySingleOrDefaultAsync<Budget>(query, new { Id = id });
        return budget;
    }

public async Task<List<BudgetWithExpenses>> GetBudgetsAsync(Guid userId)
{
    var query = @"
        SELECT 
            b.Id AS BudgetId,
            b.UserId,
            b.Month,
            b.BudgetAmount,
            e.Id AS ExpenseId,
            e.Amount,
            e.Description,
            e.Category,
            e.Date,
            e.CreatedAt
        FROM Budgets b
        LEFT JOIN Expenses e ON b.UserId = e.UserId
        WHERE b.UserId = @UserId 
        AND DATE_TRUNC('month', e.Date) = DATE_TRUNC('month', CURRENT_DATE)
        AND DATE_TRUNC('month', b.Month) = DATE_TRUNC('month', CURRENT_DATE)";
    
    var parameters = new { UserId = userId };
    
    var result = await _dbConnection.QueryAsync<BudgetWithExpenses>(query, parameters);
    
    return result.ToList();
}

}
