using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces.Persistence
{
    public interface ITmsDbContext
    {
         DbSet<Employee> Employees { get; }
         DbSet<Department> Departments { get; }
         DbSet<JobTitle> JobTitles { get; }
         DbSet<CourseCategory> CourseCategories { get; }
         DbSet<Course> Courses { get; }
         DbSet<Enrollment> Enrollments { get; }
         DbSet<TrainingCategory> TrainingCategories { get; }
         DbSet<TrainingPlan> TrainingPlans { get; }
        DbSet<TrainingClass> TrainingClasses { get; }
        DbSet<TrainingClassSchedule> TrainingClassSchedules { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
