using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Data.Seed
{
    public class TrainingPlanSeeder: IDataSeeder
    {
        private readonly ITmsDbContext _context;

        public TrainingPlanSeeder(ITmsDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Load parent first (FK safe)
            var techCategory = await _context.TrainingCategories
                .FirstOrDefaultAsync(x => x.Code == "TECH");

            if (techCategory == null)
                return;

            if (!await _context.TrainingPlans.AnyAsync(x => x.PlanCode == "TP001"))
            {
                _context.TrainingPlans.Add(new TrainingPlan
                {
                    Id = Guid.NewGuid(),
                    TrainingCategoryId = techCategory.Id,

                    PlanCode = "TP001",
                    PlanName = "Advanced C# Programming",

                    StartDate = DateTime.UtcNow.AddMonths(1),
                    EndDate = DateTime.UtcNow.AddMonths(2),

                    TrainingCompany = "TechTrain Inc.",
                    TrainingPlace = "New York",

                    TrainingCost = 1500.00m,
                    ContactPerson = "John Doe",

                    Description = "Advanced C# training covering best practices",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                });
            }

            if (!await _context.TrainingPlans.AnyAsync(x => x.PlanCode == "TP002"))
            {
                _context.TrainingPlans.Add(new TrainingPlan
                {
                    Id = Guid.NewGuid(),
                    TrainingCategoryId = techCategory.Id,

                    PlanCode = "TP002",
                    PlanName = "Effective Communication Skills",

                    StartDate = DateTime.UtcNow.AddMonths(2),
                    EndDate = DateTime.UtcNow.AddMonths(3),

                    TrainingCompany = "SoftSkills Co.",
                    TrainingPlace = "Los Angeles",

                    TrainingCost = 800.00m,
                    ContactPerson = "Jane Smith",

                    Description = "Professional communication training",
                    CreatedAt = DateTime.UtcNow,
                    isActive = true
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
