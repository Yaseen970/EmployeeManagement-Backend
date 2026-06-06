using EmployeeManagement.Application.DTOs.Auth;

namespace EmployeeManagement.Application.Interfaces.Services;  // ← Services not Repositories!

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);  // ← returns AuthResponseDto
}