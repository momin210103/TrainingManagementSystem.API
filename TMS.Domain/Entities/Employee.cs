using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Foreign Key
        public Guid? DepartmentId { get; set; }
        public Guid? JobTitleId { get; set; }

        // Navigation
        public Department? Department { get; set; } = null!;
        public JobTitle? JobTitle { get; set; } = null!;
        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<TestAttempt> TestAttempts { get; set; } = new List<TestAttempt>();
    }
}
