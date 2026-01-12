using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Attendance : BaseEntity<Guid>
    {
        
        public Guid EnrollmentId { get; set; }

        public DateTime SessionDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? MarkedAt { get; set; }

        // Navigation
        public Enrollment Enrollment { get; set; } = null!;
    }
}
