using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class QuestionConfiguration: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            //Table Name
            builder.ToTable("Questions");
            // Pk
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.QuestionText)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.Marks)
                .IsRequired();
            // Relationship
            // Test 1 - Question many
            builder.HasOne(x => x.Test)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            // Question 1 - Options many
            builder.HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
            // Question 1 - TestAnswers many
            builder.HasMany(x => x.TestAnswers)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
