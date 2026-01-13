namespace TMS.Application.Employees.Filters
{
    public class EmployeeFilter
    {
        public string? Search { get; set; }
        public Guid? DepartmentId { get; set; }
        public bool? IsActive { get; set; }
    }
}
