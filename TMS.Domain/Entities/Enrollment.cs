using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Enrollment : BaseEntity<Guid>
    {
        public string Status { get; set; } = string.Empty;
        public DateTime EnrolledAt { get; set; }
        // foreign key
        public Guid EmployeeId { get; set; }
        public Guid TrainingClassId { get; set; }

        // Navigation 
        public Employee Employee { get; set; } = null!;
        public TrainingClass TrainingClass { get; set; } = null!;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}
