using EmployeeManagement.Application.DTOs.Attendance;
using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("{employeeId}")]

        public async Task<IActionResult> GetByEmployee(
            int employeeId,
            [FromQuery] int month,
            [FromQuery] int year)
        {
            var result = await _attendanceService.GetByEmployeeAsync(employeeId, month, year);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
        {
            await _attendanceService.MarkAttendanceAsync(dto);
            return Ok(ApiResponse<string>.Ok("Marked", "Attendance marked successfully"));
        }
    }
}