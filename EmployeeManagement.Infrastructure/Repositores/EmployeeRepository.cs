using Dapper;
using EmployeeManagement.Application.DTOs.Common;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Constants;
using System.Data;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnectionFactory _db;
        public EmployeeRepository(DbConnectionFactory db) => _db = db;

        public async Task<PagedResult<Employee>> GetPagedAsync(
            int page, int pageSize, string? search)
        {
            using var connection = _db.CreateConnection();

            using var multi = await connection.QueryMultipleAsync(
                StoredProcedureNames.GetEmployeesPaged,
                new { p_page = page, p_pageSize = pageSize, p_search = search },
                commandType: CommandType.StoredProcedure
            );

            var employees = (await multi.ReadAsync<Employee>()).ToList();
            var totalCount = await multi.ReadFirstAsync<int>();

            return new PagedResult<Employee>
            {
                Data = employees,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Employee>(
                StoredProcedureNames.GetEmployeeById,
                new { p_id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            using var connection = _db.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                StoredProcedureNames.CreateEmployee,
                new
                {
                    p_first_name = employee.FirstName,
                    p_last_name = employee.LastName,
                    p_email = employee.Email,
                    p_phone = employee.Phone,
                    p_department_id = employee.DepartmentId,
                    p_job_title = employee.JobTitle,
                    p_hire_date = employee.HireDate
                },
                commandType: CommandType.StoredProcedure
            );
            return (int)result.new_id;
        }

        public async Task UpdateAsync(Employee employee)
        {
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(
                StoredProcedureNames.UpdateEmployee,
                new
                {
                    p_id = employee.Id,
                    p_first_name = employee.FirstName,
                    p_last_name = employee.LastName,
                    p_email = employee.Email,
                    p_phone = employee.Phone,
                    p_department_id = employee.DepartmentId,
                    p_job_title = employee.JobTitle,
                    p_hire_date = employee.HireDate
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(
                StoredProcedureNames.DeleteEmployee,
                new { p_id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}