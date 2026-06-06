

namespace EmployeeManagement.Application.DTOs.Auth;

// DTOs/AuthResponseDto.cs
public class AuthResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
}
