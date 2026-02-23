using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Entities;
using Npgsql;
using Dapper;

namespace BillsControl.Infrastructure.Repositories;

public class UsersRepository(string connectionString) : IUsersRepository
{
    private NpgsqlConnection CreateConnection() => new(connectionString);
    
    public async Task<UserEntity?> GetByUserName(string userName)
    {
        await using var connection = CreateConnection();
        const string sql = "SELECT " +
                           $"id AS {nameof(UserEntity.Id)}, " +
                           $"username AS {nameof(UserEntity.UserName)}, " +
                           $"password_hash AS {nameof(UserEntity.PasswordHash)}," +
                           $"role AS {nameof(UserEntity.Role)} " +
                           $"FROM users WHERE username = @{nameof(UserEntity.UserName)}";

        return await connection.QuerySingleAsync<UserEntity>(sql, new { UserName = userName });
    }

    public async Task<Guid> CreateUser(UserEntity userEntity)
    {
        await using var connection = CreateConnection();
        const string sql = "INSERT INTO users VALUES " +
                           $"(@{nameof(UserEntity.Id)}, " +
                           $"@{nameof(UserEntity.UserName)}, " +
                           $"@{nameof(UserEntity.PasswordHash)}, " +
                           $"@{nameof(UserEntity.Role)}) " +
                           "RETURNING id";

        return await connection.QuerySingleAsync<Guid>(sql, userEntity);
    }
}