using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardStats> GetStatsAsync();
    }
}