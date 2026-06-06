using EmployeeManagement.Application.DTOs.Salary;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;

        public SalaryService(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<IEnumerable<Salary>> GetReportAsync(int month, int year)
        {
            return await _salaryRepository.GetReportAsync(month, year);
        }

        public async Task CreateAsync(CreateSalaryDto dto)
        {
            var salary = new Salary
            {
                EmployeeId = dto.EmployeeId,
                BasicSalary = dto.BasicSalary,
                Allowances = dto.Allowances,
                Deductions = dto.Deductions,
                Month = dto.Month,
                Year = dto.Year
            };
            await _salaryRepository.CreateAsync(salary);
        }
    }
}