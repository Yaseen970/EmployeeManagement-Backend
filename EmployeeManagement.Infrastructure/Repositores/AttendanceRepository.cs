using Dapper;
using System.Data;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Constants;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DbConnectionFactory _db;

        public AttendanceRepository(DbConnectionFactory db) => _db = db;

        public async Task<IEnumerable<Attendance>> GetByEmployeeAsync(
            int employeeId, int month, int year)
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryAsync<Attendance>(
                StoredProcedureNames.GetAttendanceByEmployee,
                new { EmployeeId = employeeId, Month = month, Year = year },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task MarkAttendanceAsync(Attendance attendance)
        {
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(
                StoredProcedureNames.MarkAttendance,
                new
                {
                    attendance.EmployeeId,
                    attendance.Date,
                    attendance.CheckIn,
                    attendance.CheckOut,
                    attendance.Status
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}