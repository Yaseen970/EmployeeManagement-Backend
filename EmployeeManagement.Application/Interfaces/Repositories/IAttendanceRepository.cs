using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId, int month, int year);
        Task MarkAttendanceAsync(Attendance attendance);
    }
}