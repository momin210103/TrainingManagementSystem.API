using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class TrainingClass : BaseEntity<Guid>
    {
        // Key
        public Guid CourseId { get; set; }
        public Guid TrainingPlanId { get; set; }

        // Business Logic
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;

        // Navigation
        public Course Course { get; set; } = null!;
        public TrainingPlan TrainingPlan { get; set; } = null!;
        public  ICollection<TrainingClassSchedule> TrainingClassSchedules { get; set; } = new List<TrainingClassSchedule>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
