using Dapper;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using System.Data;

namespace EmployeeManagement.Infrastructure.Repositores;

public class AuthRepository : IAuthRepository
{
    private readonly DbConnectionFactory _db;

    public AuthRepository(DbConnectionFactory db) => _db = db;

    // ── GET USER FOR LOGIN ─────────────────────────────────
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        using var connection = _db.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "sp_LoginUser",
            new { p_Username = username },
            commandType: CommandType.StoredProcedure
        );
    }

    // ── REGISTER ───────────────────────────────────────────
    public async Task<(bool Success, string Message, int UserId)> RegisterAsync(
        string fullName, string email, string username,
        string passwordHash, string role)
    {
        using var connection = _db.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync(
            "sp_RegisterUser",
            new
            {
                p_FullName = fullName,
                p_Email = email,
                p_Username = username,
                p_PasswordHash = passwordHash,
                p_Role = role
            },
            commandType: CommandType.StoredProcedure
        );

        if (result == null)
            return (false, "Unexpected error", 0);

        bool success = Convert.ToInt32(result.Success) == 1;
        string message = result.Message?.ToString() ?? "";
        int userId = result.UserId == null ? 0 : Convert.ToInt32(result.UserId);

        return (success, message, userId);
    }
}