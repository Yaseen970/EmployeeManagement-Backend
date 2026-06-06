namespace EmployeeManagement.Application.DTOs.Attendance
{
    public class MarkAttendanceDto
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public string Status { get; set; } = "Present";
    }
}