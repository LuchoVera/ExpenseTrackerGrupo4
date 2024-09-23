using Dapper;
using System.Data;

namespace ExpenseTrackerGrupo4.src.Domain.Contexts;

public class BaseContext(IDbConnection connection)
{
    private readonly IDbConnection _connection = connection;

    public IEnumerable<T> Query<T>(string sql, object? parameters = null)
    {
        return _connection.Query<T>(sql, parameters);
    }

    public int Execute(string sql, object? parameters = null)
    {
        return _connection.Execute(sql, parameters);
    }
}
