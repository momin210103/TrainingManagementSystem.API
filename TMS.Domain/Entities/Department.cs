using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Department : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
