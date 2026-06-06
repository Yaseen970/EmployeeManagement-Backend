using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Application.Interfaces.Repositories;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardService;

        public DashboardController(IDashboardRepository dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var result = await _dashboardService.GetStatsAsync();
            return Ok(ApiResponse<object>.Ok(result));
        }
    }
}