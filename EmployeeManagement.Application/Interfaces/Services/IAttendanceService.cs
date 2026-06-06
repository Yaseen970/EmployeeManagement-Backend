using EmployeeManagement.Application.DTOs.Attendance;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetByEmployeeAsync(
            int employeeId,
            int month,
            int year);

        Task MarkAttendanceAsync(MarkAttendanceDto dto);
    }
}