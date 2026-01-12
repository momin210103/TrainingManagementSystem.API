using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TestAttemptConfiguration: IEntityTypeConfiguration<TestAttempt>
    {
        public void Configure(EntityTypeBuilder<TestAttempt> builder)
        {
            // Table Name
            builder.ToTable("TestAttempts");
            // Pk
            builder.HasKey(x => x.Id);
            // Propety
            builder.Property(x => x.SubmittedAt)
                .IsRequired(false);
            builder.Property(x => x.StartedAt)
                .IsRequired();
            builder.Property(x => x.Score)
                .IsRequired();
                
            // Relationship
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.TestAttempts)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Test)
                .WithMany(x => x.TestAttempts)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.TestAnswers)
                .WithOne(x => x.TestAttempt)
                .HasForeignKey(x => x.TestAttemptId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
