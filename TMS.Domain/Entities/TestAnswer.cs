using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class TestAnswer : BaseEntity<Guid>
    {
        // Foreign Key
        public Guid TestAttemptId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid SelectedOptionId { get; set; }

        // Navigation
        public TestAttempt TestAttempt { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public Option SelectedOption { get; set; } = null!;
    }
}
