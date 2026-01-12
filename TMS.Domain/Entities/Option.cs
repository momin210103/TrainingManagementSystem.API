using TMS.Domain.Common;

namespace TMS.Domain.Entities
{
    public class Option : BaseEntity<Guid>
    {
        // Foreign Key
        public Guid QuestionId { get; set; }

        // Business Fields
        public string OptionKey { get; set; } = string.Empty;
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }

        // Navigation
        public Question Question { get; set; } = null!;
        
    }
}
