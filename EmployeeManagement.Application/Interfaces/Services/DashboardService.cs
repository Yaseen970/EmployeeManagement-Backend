using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public DashboardService(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IAttendanceRepository attendanceRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<DashboardStats> GetStatsAsync()
        {
            var employees = await _employeeRepository.GetPagedAsync(1, 10000, null);
            var departments = await _departmentRepository.GetAllAsync();

            var newHires = employees.Data
                .Count(e => e.HireDate.Month == DateTime.Now.Month
                         && e.HireDate.Year == DateTime.Now.Year);

            return new DashboardStats
            {
                TotalEmployees = employees.TotalCount,
                TotalDepartments = departments.Count(),
                PresentToday = 0,
                NewHiresThisMonth = newHires
            };
        }
    }
}