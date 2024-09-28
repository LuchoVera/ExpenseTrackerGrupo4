using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;
public class BudgetNotificationLogRepository : IBudgetNotificationLogRepository
{
    private readonly IDbConnection _dbConnection;

    public BudgetNotificationLogRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<BudgetNotificationLog?> GetByBudgetIdAsync(Guid budgetId, Guid userId)
    {
        var sql = @"
            SELECT UserId AS UserId, BudgetId AS BudgetId, 
                   Notified_80 AS Notified80, Notified_90 AS Notified90, Notified_100 AS Notified100
            FROM BudgetNotificationLog
            WHERE BudgetId = @BudgetId AND UserId = @UserId;
        ";

        return await _dbConnection.QueryFirstOrDefaultAsync<BudgetNotificationLog>(sql, new { BudgetId = budgetId, UserId = userId });
    }

    public async Task InsertOrUpdateAsync(BudgetNotificationLog log)
    {
        var sql = @"
            INSERT INTO BudgetNotificationLog (UserId, BudgetId, Notified_80, Notified_90, Notified_100)
            VALUES (@UserId, @BudgetId, @Notified80, @Notified90, @Notified100)
            ON CONFLICT (UserId, BudgetId)
            DO UPDATE 
            SET Notified_80 = EXCLUDED.Notified_80, 
                Notified_90 = EXCLUDED.Notified_90, 
                Notified_100 = EXCLUDED.Notified_100;
        ";

        await _dbConnection.ExecuteAsync(sql, new {
            log.UserId,
            log.BudgetId,
            log.Notified80,
            log.Notified90,
            log.Notified100
        });
    }
}
