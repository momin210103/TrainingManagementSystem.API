using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class OptionConfiguration: IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("Options");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.OptionKey)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.OptionText)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.IsCorrect);

            //Relationship
            builder.HasOne(x => x.Question)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
