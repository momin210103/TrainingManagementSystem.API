using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Table Name ( explicit is better in production )
            builder.ToTable("Courses");
            // Primary key
            builder.HasKey(c => c.Id);
            // Properties 
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.CourseCode)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.CourseCode)
                .IsUnique();
            builder.Property(c => c.Description)
                .HasMaxLength(1000);
            builder.Property(c => c.DurationHours)
                .IsRequired();
            // Relationship : Course Category (1) -> Course (many)
            builder.HasOne(c => c.CourseCategory)
                .WithMany(cc => cc.Courses)
                .HasForeignKey(c => c.CourseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // Relationship : Course (1) -> TraininClasses(many)
            builder.HasMany(c => c.TrainingClasses)
                .WithOne(tc => tc.Course)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
