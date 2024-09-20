using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class UserRepository(IDbConnection connection) : IUserRepository
{
    private readonly IDbConnection _connection = connection;

    public Task<User?> GetUserById(Guid id)
    {
        string sql = "SELECT * FROM Users WHERE Id = @Id";
        return _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task AddUser(User user)
    {
        var query =
            "INSERT INTO Users (Id, Name, Email, PasswordHash, CreatedAt) VALUES (@Id, @Name, @Email, @PasswordHash, @CreatedAt)";
        await _connection.ExecuteAsync(query, user);
    }

    public async Task UpdateUser(User user)
    {
        var query =
            "UPDATE Users SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash WHERE Id = @Id";
        await _connection.ExecuteAsync(query, user);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var query = "SELECT * FROM Users WHERE Email = @Email";

        return await _connection.QueryFirstOrDefaultAsync<User>(
            query,
            new { Email = email.ToLower() }
        );
    }
}
