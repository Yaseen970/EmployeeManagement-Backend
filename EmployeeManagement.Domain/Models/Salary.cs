namespace EmployeeManagement.Domain.Models
{
    public class Salary
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetSalary { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
