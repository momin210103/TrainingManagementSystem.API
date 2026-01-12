using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Question : BaseEntity<Guid>
    {
        // Fk
        public Guid TestId { get; set; }
        // Business Fields
        public string QuestionText { get; set; } = string.Empty;
        public int Marks { get; set; }

        // Navigation 
        public Test Test { get; set; } = null!;
        public ICollection<Option> Options { get; set; } = new List<Option>();
        public ICollection<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
