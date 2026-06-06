using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<(bool Success, string Message, int UserId)> RegisterAsync(
        string fullName, string email, string username,
        string passwordHash, string role);
}