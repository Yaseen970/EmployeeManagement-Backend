namespace EmployeeManagement.Application.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int? DepartmentId { get; set; }
        public string? JobTitle { get; set; }
        public DateTime HireDate { get; set; }
    }
}