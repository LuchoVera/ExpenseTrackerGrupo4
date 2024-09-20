using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using System.Data;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;

    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public User? GetUserById(Guid id)
    {
        string sql = "SELECT * FROM Users WHERE Id = @Id";
        return _connection.QuerySingleOrDefault<User>(sql, new { Id = id });
    }

    public void AddUser(User user)
    {
        string sql = @"
            INSERT INTO Users (Id, Name, Email, PasswordHash, CreatedAt) 
            VALUES (@Id, @Name, @Email, @PasswordHash, @CreatedAt)";
        
        _connection.Execute(sql, new
        {
            user.Id,
            user.Name,
            user.Email,
            user.PasswordHash,
            user.CreatedAt
        });
    }

    public void UpdateUser(User user)
    {
        string sql = @"
            UPDATE Users 
            SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash 
            WHERE Id = @Id";
        
        _connection.Execute(sql, new
        {
            user.Name,
            user.Email,
            user.PasswordHash,
            user.Id
        });
    }
}
