using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardStats> GetStatsAsync();
    }
}