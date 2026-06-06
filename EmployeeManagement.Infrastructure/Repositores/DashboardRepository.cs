using Dapper;
using System.Data;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Constants;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbConnectionFactory _db;

        public DashboardRepository(DbConnectionFactory db) => _db = db;

        public async Task<DashboardStats> GetStatsAsync()
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DashboardStats>(
                StoredProcedureNames.GetDashboardStats,
                commandType: CommandType.StoredProcedure
            ) ?? new DashboardStats();
        }
    }
}