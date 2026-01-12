using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Table name
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
