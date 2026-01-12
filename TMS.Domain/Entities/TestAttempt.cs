using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class TestAttempt : BaseEntity<Guid>
    {
        // Foreign key
        public Guid EmployeeId { get; set; }
        public Guid TestId { get; set; }

        // Business Login
        public DateTime? SubmittedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public int Score { get; set; }
        

        // Navigation
        public Employee Employee { get; set; } = null!;

        public Test Test { get; set; } = null!;

        public ICollection<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();



    }
}
