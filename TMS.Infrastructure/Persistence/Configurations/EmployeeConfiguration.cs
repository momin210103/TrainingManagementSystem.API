using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Table Name
            builder.ToTable("Employees");
            // Primary key
            builder.HasKey(x => x.Id);

            // Property
            builder.Property(x => x.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.HasIndex(x => x.EmployeeCode)
                .IsUnique();
            // ForeignKey
            builder.Property(x => x.DepartmentId).IsRequired(false);
            builder.Property(x => x.JobTitleId).IsRequired(false);
            // Relationship
            builder.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<JobTitle>()
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.JobTitleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.EmployeeRoles)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Enrollments)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.TestAttempts)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
