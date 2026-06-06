using EmployeeManagement.Application.DTOs.Salary;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface ISalaryService
    {
        Task<IEnumerable<Salary>> GetReportAsync(int month, int year);
        Task CreateAsync(CreateSalaryDto dto);
    }
}