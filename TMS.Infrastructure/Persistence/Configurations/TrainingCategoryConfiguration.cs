using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TrainingCategoryConfiguration: IEntityTypeConfiguration<TrainingCategory>
    {
        public void Configure(EntityTypeBuilder<TrainingCategory> builder)
        {
            // Table Name
            builder.ToTable("TrainingCategories");
            // Pk
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);
            // Relationship
            builder.HasMany(x => x.TrainingPlans)
                .WithOne(x => x.TrainingCategory)
                .HasForeignKey(x => x.TrainingCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
