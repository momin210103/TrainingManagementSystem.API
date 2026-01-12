using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class CourseCategoryConfiguration: IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            // Table name
            builder.ToTable("CourseCategories");
            // pk
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.HasIndex(x => x.Code)
                .IsUnique();
            // Relationshi
            builder.HasMany(x => x.Courses)
                .WithOne(x => x.CourseCategory)
                .HasForeignKey(x => x.CourseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
