namespace TMS.Application.Departments.DTOs
{
    public class EmployeeItemDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }
}
