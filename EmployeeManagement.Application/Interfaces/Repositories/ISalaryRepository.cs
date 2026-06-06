using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetReportAsync(int month, int year);
        Task CreateAsync(Salary salary);
    }
}