namespace EmployeeManagement.Application.DTOs.Salary
{
    public class CreateSalaryDto
    {
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}