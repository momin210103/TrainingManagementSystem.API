using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");
            // Primary Key
            builder.HasKey(x => x.Id);
            // Properties
            builder.Property(x => x.EnrolledAt)
                .IsRequired();
            builder.Property(x => x.Status)
              .IsRequired()
              .HasMaxLength(50);

            // Prevent duplicate enrollment
            builder.HasIndex(x => new { x.EmployeeId, x.TrainingClassId })
                   .IsUnique();

            // Relationship
            // Enrollment 1 -> Employee many
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.TrainingClass)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.TrainingClassId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Attendances)
                .WithOne(x => x.Enrollment)
                .HasForeignKey(x => x.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
