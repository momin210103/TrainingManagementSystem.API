using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TrainingClassConfiguration : IEntityTypeConfiguration<TrainingClass>
    {
        public void Configure(EntityTypeBuilder<TrainingClass> builder)
        {
            // Table Name
            builder.ToTable("TrainingClasses");
            // Primary Key
            builder.HasKey(tc => tc.Id);
            // Property
            builder.Property(tc => tc.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(tc => tc.StartDate)
                .IsRequired();
                
            builder.Property(tc => tc.EndDate)
                .IsRequired();
            builder.Property(tc => tc.Capacity)
                .IsRequired();
            builder.Property(tc => tc.Status)
                .IsRequired()
                .HasMaxLength(50);
            // Relationship 
            // Course (1) -> TrainingClass (Many)
            builder.HasOne(c => c.Course)
                .WithMany(tc => tc.TrainingClasses)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            // TrainingPlan (1) - TrainingClass (Many)
            builder.HasOne(tc => tc.TrainingPlan)
                .WithMany(tp => tp.TrainingClasses)
                .HasForeignKey(tc => tc.TrainingPlanId)
                .OnDelete(DeleteBehavior.Restrict);
            // Enrollment 1 -> TrainingClass many
            builder.HasMany(tc => tc.Enrollments)
                .WithOne(e => e.TrainingClass)
                .HasForeignKey(e => e.TrainingClassId)
                .OnDelete(DeleteBehavior.Cascade);
            // TrainingClass (1) -> Schedules (Many)
            builder.HasMany(tc => tc.TrainingClassSchedules)
                .WithOne(s => s.TrainingClass)
                .HasForeignKey(s => s.TrainingClassId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
