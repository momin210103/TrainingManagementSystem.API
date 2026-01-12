namespace TMS.Application.Employees.DTOs
{
    public class CreateEmployeeRequest
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Guid? DepartmentId { get; set; }
        public Guid? JobTitleId { get; set; }
    }
}
