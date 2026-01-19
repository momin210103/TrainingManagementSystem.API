namespace TMS.Application.Departments.DTOs
{
    public class DepartmentEmployeeResponse
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int TotalEmployees { get; set; }
        public List<EmployeeItemDTO> Employees { get; set; } = [];
    }
}
