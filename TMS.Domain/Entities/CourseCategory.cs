using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class CourseCategory : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
