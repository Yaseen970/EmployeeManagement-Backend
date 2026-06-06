using Dapper;
using System.Data;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Constants;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbConnectionFactory _db;

        public DepartmentRepository(DbConnectionFactory db) => _db = db;

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryAsync<Department>(
                StoredProcedureNames.GetAllDepartments,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Department>(
                StoredProcedureNames.GetDepartmentById,
                new { p_id = id },  // 👈 fixed
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(Department department)
        {
            using var connection = _db.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<int>(
                StoredProcedureNames.CreateDepartment,
                new { p_name = department.Name, p_description = department.Description }, // 👈 fixed
                commandType: CommandType.StoredProcedure
            );
        }
    }
    }