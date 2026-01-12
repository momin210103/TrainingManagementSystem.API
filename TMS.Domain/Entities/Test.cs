using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Test : BaseEntity<Guid>
    {
        // Foreign Key
        public Guid CourseId { get; set; }
        // Business Logic
        public string Title { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
        public int DurationMinutes { get; set; }

        // Navigation
        public Course Course { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<TestAttempt> TestAttempts { get; set; } = new List<TestAttempt>();
    }
}
