using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.DTOs.Department;
using EmployeeManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            var id = await _departmentService.CreateAsync(dto);
            return Ok(ApiResponse<object>.Ok(new { id }, "Department created successfully"));
        }
    }
}