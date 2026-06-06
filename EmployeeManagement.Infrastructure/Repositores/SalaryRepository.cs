using Dapper;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Constants;
using System.Data;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly DbConnectionFactory _db;
        public SalaryRepository(DbConnectionFactory db) => _db = db;

        public async Task<IEnumerable<Salary>> GetReportAsync(int month, int year)
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryAsync<Salary>(
                StoredProcedureNames.GetSalaryReport,
                new { p_month = month, p_year = year },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task CreateAsync(Salary salary)
        {
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(
                StoredProcedureNames.CreateSalary,
                new
                {
                    p_employee_id = salary.EmployeeId,   
                    p_basic_salary = salary.BasicSalary,
                    p_allowances = salary.Allowances,
                    p_deductions = salary.Deductions,
                    p_month = salary.Month,
                    p_year = salary.Year
                },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}