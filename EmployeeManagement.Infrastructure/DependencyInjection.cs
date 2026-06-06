using Dapper;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Infrastructure.Repositores;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<DbConnectionFactory>();

        // ── Repositories ──────────────────────────────────
        services.AddScoped<IAuthRepository, AuthRepository>();      // ← ADD THIS LINE
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<ISalaryRepository, SalaryRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        return services;
    }
}