using EmployeeManagement.Application.DTOs.Department;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<int> CreateAsync(CreateDepartmentDto dto);
    }
}