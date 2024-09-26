using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly IDbConnection _dbConnection;

    public GoalRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task AddAsync(Goal goal)
    {
        var query = @"
            INSERT INTO Goals (Id, UserId, GoalAmount, Deadline, CurrentAmount) 
            VALUES (@Id, @UserId, @GoalAmount, @Deadline, @CurrentAmount)";
        
        await _dbConnection.ExecuteAsync(query, goal);
    }

    public async Task UpdateAsync(Goal goal)
    {
        var query = @"
            UPDATE Goals 
            SET GoalAmount = @GoalAmount, Deadline = @Deadline, CurrentAmount = @CurrentAmount
            WHERE Id = @Id AND UserId = @UserId";
        
        await _dbConnection.ExecuteAsync(query, goal);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "DELETE FROM Goals WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Goal?> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM Goals WHERE Id = @Id";
        var goal = await _dbConnection.QuerySingleOrDefaultAsync<Goal>(query, new { Id = id });
        return goal;
    }

    public async Task<List<GoalWithDetails>> GetGoalsWithDetailsAsync(Guid userId)
    {
        var query = @"
            SELECT 
                g.Id AS GoalId,
                g.GoalAmount,
                g.Deadline,
                g.CurrentAmount,
                g.CreatedAt,
                COALESCE(SUM(i.Amount), 0) AS TotalIncome,
                COALESCE(SUM(e.Amount), 0) AS TotalExpenses
            FROM Goals g
            LEFT JOIN Incomes i ON g.UserId = i.UserId AND DATE_TRUNC('month', i.Date) = DATE_TRUNC('month', CURRENT_DATE)
            LEFT JOIN Expenses e ON g.UserId = e.UserId AND DATE_TRUNC('month', e.Date) = DATE_TRUNC('month', CURRENT_DATE)
            WHERE g.UserId = @UserId
            GROUP BY g.Id, g.GoalAmount, g.Deadline, g.CurrentAmount, g.CreatedAt";

        var parameters = new { UserId = userId };

        var result = await _dbConnection.QueryAsync<GoalWithDetails>(query, parameters);
        
        return result.ToList();
    }
}
