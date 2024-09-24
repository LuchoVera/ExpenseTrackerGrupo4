using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
namespace ExpenseTrackerGrupo4.src.Database.Configuration
{

    public class DBConnection : IDbConnectionFactory
    {
        private DBOptions _options;
        public DBConnection(IOptions<DBOptions> options)
        {
            _options = options.Value;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new NpgsqlConnection(_options.DefaultConnection);
            await connection.OpenAsync();
            return connection;
        }
    }
}