using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class TrainingClassSchedule : BaseEntity<Guid>
    {
        // Foreign Key
        public Guid TrainingClassId { get; set; }

        // Business Fields

        public DateTime SessionDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Navigation
        public TrainingClass TrainingClass { get; set; } = null!;

    }
}
