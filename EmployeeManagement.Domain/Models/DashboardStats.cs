namespace EmployeeManagement.Domain.Models
{
    public class DashboardStats
    {
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int PresentToday { get; set; }
        public int NewHiresThisMonth { get; set; }
    }
}
