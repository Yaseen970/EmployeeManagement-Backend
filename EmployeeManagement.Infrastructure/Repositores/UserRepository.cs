using System.Data;
using MySql.Data.MySqlClient;                                  // ← replace System.Data.SqlClient
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Application.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<(bool Success, string Message, int UserId)> RegisterAsync(
        string fullName, string email, string username,
        string passwordHash, string role)
    {
        using var conn = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand("sp_RegisterUser", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("p_FullName", fullName);
        cmd.Parameters.AddWithValue("p_Email", email);
        cmd.Parameters.AddWithValue("p_Username", username);
        cmd.Parameters.AddWithValue("p_PasswordHash", passwordHash);
        cmd.Parameters.AddWithValue("p_Role", role);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            bool success = Convert.ToInt32(reader["Success"]) == 1;
            string message = reader["Message"].ToString()!;
            int userId = reader["UserId"] == DBNull.Value
                             ? 0 : Convert.ToInt32(reader["UserId"]);
            return (success, message, userId);
        }

        return (false, "Unexpected error", 0);
    }

    public async Task<User?> GetUserForLoginAsync(string username)
    {
        using var conn = new MySqlConnection(_connectionString);
        using var cmd = new MySqlCommand("sp_LoginUser", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("p_Username", username);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                FullName = reader["FullName"].ToString()!,
                Email = reader["Email"].ToString()!,
                Username = reader["Username"].ToString()!,
                PasswordHash = reader["PasswordHash"].ToString()!,
                Role = reader["Role"].ToString()!,
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }

        return null;
    }
}