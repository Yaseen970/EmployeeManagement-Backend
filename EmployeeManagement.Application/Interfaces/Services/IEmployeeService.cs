using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<PagedResult<Employee>> GetPagedAsync(
            int page,
            int pageSize,
            string? search);

        Task<Employee?> GetByIdAsync(int id);

        Task<int> CreateAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(int id);
    }
}