using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TestConfiguration: IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Tests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200);
            builder.Property(x => x.TotalMarks)
                .IsRequired();
            builder.Property(x => x.DurationMinutes)
                .IsRequired();
            // Relationship
            builder.HasOne(x => x.Course)
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Questions)
                .WithOne(x => x.Test)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.TestAttempts)
                .WithOne(x => x.Test)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
