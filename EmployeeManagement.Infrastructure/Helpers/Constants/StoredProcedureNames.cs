namespace EmployeeManagement.Infrastructure.Constants
{
    public static class StoredProcedureNames
    {
        // Employee
        public const string GetEmployeesPaged = "sp_GetEmployeesPaged";
        public const string GetEmployeeById = "sp_GetEmployeeById";
        public const string CreateEmployee = "sp_CreateEmployee";
        public const string UpdateEmployee = "sp_UpdateEmployee";
        public const string DeleteEmployee = "sp_DeleteEmployee";

        // Department
        public const string GetAllDepartments = "sp_GetAllDepartments";
        public const string GetDepartmentById = "sp_GetDepartmentById";
        public const string CreateDepartment = "sp_CreateDepartment";

        // Attendance
        public const string GetAttendanceByEmployee = "sp_GetAttendanceByEmployee";
        public const string MarkAttendance = "sp_MarkAttendance";

        // Salary
        public const string GetSalaryReport = "sp_GetSalaryReport";
        public const string CreateSalary = "sp_CreateSalary";

        // Auth
        public const string GetUserByUsername = "sp_GetUserByUsername";

        // Dashboard
        public const string GetDashboardStats = "sp_GetDashboardStats";
        // Auth
        public const string RegisterUser = "sp_RegisterUser";
    }
}