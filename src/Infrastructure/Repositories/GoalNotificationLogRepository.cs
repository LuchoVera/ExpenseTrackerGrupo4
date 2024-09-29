using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories
{
    public class GoalNotificationLogRepository : IGoalNotificationLogRepository
    {
        private readonly IDbConnection _dbConnection;

        public GoalNotificationLogRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GoalNotificationLog?> GetByGoalIdAsync(Guid goalId, Guid userId)
        {
            var query = "SELECT * FROM GoalNotificationLogs WHERE GoalId = @GoalId AND UserId = @UserId";
            return await _dbConnection.QuerySingleOrDefaultAsync<GoalNotificationLog>(query, new { GoalId = goalId, UserId = userId });
        }

        public async Task InsertOrUpdateAsync(GoalNotificationLog log)
        {
            var query = @"
                INSERT INTO GoalNotificationLogs (Id, UserId, GoalId, Notified50, Notified100) 
                VALUES (@Id, @UserId, @GoalId, @Notified50, @Notified100)
                ON CONFLICT (UserId, GoalId) DO UPDATE
                SET Notified50 = @Notified50, Notified100 = @Notified100";

            await _dbConnection.ExecuteAsync(query, log);
        }

    }
}
