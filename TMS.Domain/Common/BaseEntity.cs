
namespace TMS.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool isActive { get; set; }
    }
}
