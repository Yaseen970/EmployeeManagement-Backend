using EmployeeManagement.Domain.Models;
namespace EmployeeManagement.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int id);

        Task<int> CreateAsync(Department department);
    }
}