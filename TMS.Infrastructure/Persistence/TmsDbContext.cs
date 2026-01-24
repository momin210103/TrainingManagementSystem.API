using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence
{
    public class TmsDbContext:DbContext, ITmsDbContext
    {
        public TmsDbContext(DbContextOptions<TmsDbContext> options) : base(options)
        {

        }
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<JobTitle> JobTitles => Set<JobTitle>();
        public DbSet<Option> Options => Set<Option>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Test> Tests => Set<Test>();
        public DbSet<TestAnswer> TestAnswers => Set<TestAnswer>();
        public DbSet<TestAttempt> TestAttempts => Set<TestAttempt>();
        public DbSet<TrainingCategory> TrainingCategories => Set<TrainingCategory>();
        public DbSet<TrainingClass> TrainingClasses => Set<TrainingClass>();
        public DbSet<TrainingClassSchedule> TrainingClassSchedules => Set<TrainingClassSchedule>();
        public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();
        public DbSet<Question> Questions => Set<Question>();

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TmsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>()
                .HasQueryFilter(d => d.isActive);
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(d => d.isActive);
            modelBuilder.Entity<CourseCategory>()
                .HasQueryFilter(x => x.isActive);
            modelBuilder.Entity<Course>()
                .HasQueryFilter(x => x.isActive);
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Enrollment>()
                .HasQueryFilter(x => x.isActive);
            modelBuilder.Entity<TrainingCategory>()
                .HasQueryFilter(x => x.isActive);
            modelBuilder.Entity<TrainingPlan>().HasQueryFilter(x => x.isActive);
            modelBuilder.Entity<TrainingClass>()
                .HasQueryFilter(x => x.isActive);
        }
    }
}
