using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<(bool Success, string Message, int UserId)> RegisterAsync(
            string fullName, string email, string username,
            string passwordHash, string role);

        Task<Models.User?> GetUserForLoginAsync(string username);
    }
}