using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class TrainingCategory : BaseEntity<Guid>
    {
        // Business Fields
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation
        public ICollection<TrainingPlan> TrainingPlans { get; set; } = new List<TrainingPlan>();
    }
}
