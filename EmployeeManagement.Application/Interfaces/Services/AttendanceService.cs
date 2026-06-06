using EmployeeManagement.Application.DTOs.Attendance;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<Attendance>> GetByEmployeeAsync(
            int employeeId,
            int month,
            int year)
        {
            return await _attendanceRepository.GetByEmployeeAsync(
                employeeId,
                month,
                year);
        }

        public async Task MarkAttendanceAsync(MarkAttendanceDto dto)
        {
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                Status = dto.Status
            };

            await _attendanceRepository.MarkAttendanceAsync(attendance);
        }
    }
}