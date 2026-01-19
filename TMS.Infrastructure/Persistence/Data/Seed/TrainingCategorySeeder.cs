using TMS.Application.Common.Interfaces.Persistence;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Data.Seed
{
    public class TrainingCategorySeeder : IDataSeeder
    {
        private readonly ITmsDbContext _context;
        public TrainingCategorySeeder(ITmsDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            if(_context.TrainingCategories.Any())
            {
                return;
            }
            var categories = new List<TrainingCategory>
            {
                new TrainingCategory{
                    Id = Guid.NewGuid(),
                    Code = "TECH",
                    Name = "Technical Training",
                    Description = "Training focused on technical skills and knowledge.",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingCategory{
                    Id = Guid.NewGuid(),
                    Code = "SOFT",
                    Name = "Soft Skills Training",
                    Description = "Training aimed at improving interpersonal and communication skills.",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingCategory{
                    Id = Guid.NewGuid(),
                    Code = "LEAD",
                    Name = "Leadership Training",
                    Description = "Training designed to develop leadership and management skills.",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingCategory{
                    Id = Guid.NewGuid(),
                    Code = "COMPL",
                    Name = "Compliance Training",
                    Description = "Training to ensure understanding of laws, regulations, and company policies.",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                
            };
            await _context.TrainingCategories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }
    }
}
