using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;          // ← ADD THIS - fixes CS1503 and CS1061
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EmployeeManagement.Application.DTOs.Auth;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Application.Interfaces.Services;
using DomainUser = EmployeeManagement.Domain.Models.User;  // ← alias to avoid conflict

namespace EmployeeManagement.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthService(IAuthRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            return new AuthResponseDto
            {
                Success = false,
                Message = "Passwords do not match"
            };

        string hash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12);

        var (success, message, _) = await _repo.RegisterAsync(
            dto.FullName, dto.Email, dto.Username, hash, "Employee");

        return new AuthResponseDto { Success = success, Message = message };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _repo.GetUserByUsernameAsync(dto.Username);

        if (user == null)
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password"
            };

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!passwordMatches)
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password"
            };

        string token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful",
            Data = new LoginResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            }
        };
    }

    private string GenerateJwtToken(DomainUser user)  // ← uses alias
    {
        var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // ← These are System.Security.Claims.Claim — now correct
        var claims = new List<Claim>
        {
            new Claim("userId",              user.UserId.ToString()),
            new Claim("username",            user.Username),
            new Claim("email",               user.Email),
            new Claim("role",                user.Role),
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}