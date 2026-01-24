using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Data.Seed
{
    public class TrainingClassSeeder: IDataSeeder
    {
        private readonly ITmsDbContext _context;
        public TrainingClassSeeder(ITmsDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            if( await _context.TrainingClasses.AnyAsync())
            {
                return;
            }
            // Load parent first (FK safe)
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseCode == "CSE-4101");
            var plan = await _context.TrainingPlans.FirstOrDefaultAsync(x =>x.PlanCode == "TP001");
            if (course == null)
                return;
            if(plan == null)
                return;
            var trainingClasses = new List<TrainingClass>
            {
                new TrainingClass{
                    Id = Guid.NewGuid(),
                    Name = "Class-1",
                    CourseId = course.Id,
                    TrainingPlanId = plan.Id,                
                    StartDate = DateTime.UtcNow.AddMonths(1),
                    EndDate = DateTime.UtcNow.AddMonths(2),
                    Capacity = 25,
                    Status = "Scheduled",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingClass{
                    Id = Guid.NewGuid(),
                    Name = "Class-2",
                    CourseId = course.Id,
                    TrainingPlanId = plan.Id,
                    StartDate = DateTime.UtcNow.AddMonths(2),
                    EndDate = DateTime.UtcNow.AddMonths(3),
                    Capacity = 25,
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingClass{
                    Id = Guid.NewGuid(),
                    Name = "Class 3",
                    CourseId = course.Id,
                    TrainingPlanId = plan.Id,
                    
                    StartDate = DateTime.UtcNow.AddMonths(3),
                    EndDate = DateTime.UtcNow.AddMonths(4),
                    
                    Capacity = 25,
                    Status = "Scheduled",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingClass{
                    Id = Guid.NewGuid(),
                    Name = "Class 4",
                    CourseId = course.Id,
                    TrainingPlanId = plan.Id,
                    
                    StartDate = DateTime.UtcNow.AddMonths(4),
                    EndDate = DateTime.UtcNow.AddMonths(5),
                    
                    Capacity = 25,
                    Status = "Scheduled",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                },
                new TrainingClass{
                    Id = Guid.NewGuid(),
                    Name = "Class 5",
                    CourseId = course.Id,
                    TrainingPlanId = plan.Id,
                    
                    StartDate = DateTime.UtcNow.AddMonths(5),
                    EndDate = DateTime.UtcNow.AddMonths(6),
                    
                    Capacity = 25,
                    Status = "Scheduled",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                }

            };
            await _context.TrainingClasses.AddRangeAsync(trainingClasses);
            await _context.SaveChangesAsync();

        }
    }
}
