using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.DTOs.Employee;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            var result = await _employeeService.GetPagedAsync(page, pageSize, search);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound(ApiResponse<string>.Fail("Employee not found"));

            return Ok(ApiResponse<object>.Ok(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                Email = dto.Email,
            };

            var id = await _employeeService.CreateAsync(employee);

            return Ok(ApiResponse<object>.Ok(
                new { id },
                "Employee created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Id = id,
                FirstName = dto.FirstName,
                Email = dto.Email,
            };

            await _employeeService.UpdateAsync(employee);
            return Ok(ApiResponse<string>.Ok("Updated", "Employee updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("Deleted", "Employee deleted successfully"));
        }
    }
}