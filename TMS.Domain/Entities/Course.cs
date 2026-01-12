

using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Course : BaseEntity<Guid>
    {
        // Foreign Key
        public Guid CourseCategoryId { get; set; }

        // Business Logic 
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationHours { get; set; }

        // Navigation
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public CourseCategory CourseCategory { get; set; } = null!;
        public ICollection<TrainingClass> TrainingClasses { get; set; } = new List<TrainingClass>();

    }
}
