using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.DTOs.Salary;
using EmployeeManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(
            [FromQuery] int month,
            [FromQuery] int year)
        {
            var result = await _salaryService.GetReportAsync(month, year);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSalaryDto dto)
        {
            await _salaryService.CreateAsync(dto);
            return Ok(ApiResponse<string>.Ok("Created", "Salary record created successfully"));
        }
    }
}