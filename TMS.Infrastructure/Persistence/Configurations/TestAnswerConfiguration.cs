
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TestAnswerConfiguration: IEntityTypeConfiguration<TestAnswer>
    {
        public void Configure(EntityTypeBuilder<TestAnswer> builder)
        {
            // Table Name
            builder.ToTable("TestAnswers");
            // Pk 
            builder.HasKey(x => x.Id);
            // Foreign Key 
            builder.HasOne(x => x.TestAttempt)
                .WithMany(x => x.TestAnswers)
                .HasForeignKey(x => x.TestAttemptId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Question)
                .WithMany(x => x.TestAnswers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SelectedOption)
                .WithMany()
                .HasForeignKey(x => x.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
