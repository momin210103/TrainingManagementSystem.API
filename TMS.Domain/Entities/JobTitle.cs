

using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class JobTitle : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
