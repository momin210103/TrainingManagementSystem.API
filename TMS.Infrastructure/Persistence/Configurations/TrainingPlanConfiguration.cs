

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TrainingPlanConfiguration: IEntityTypeConfiguration<TrainingPlan>
    {
        public void Configure(EntityTypeBuilder<TrainingPlan> builder)
        {
            // Table Name
            builder.ToTable("TrainingPlans");
            // Pk
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.PlanCode)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.PlanName)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasIndex(x => x.PlanCode)
                .IsUnique();
            builder.HasIndex(x => x.PlanName)
                .IsUnique();
            builder.Property(x => x.StartDate)
                .IsRequired();
            builder.Property(x => x.EndDate)
                .IsRequired();
            builder.Property(x => x.TrainingCompany)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.TrainingPlace)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.TrainingCost)
                .HasColumnType("decimal(28,2)")
                .IsRequired();
            builder.Property(x => x.ContactPerson)
                .IsRequired();
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            // Relationship
            builder.HasOne(x => x.TrainingCategory)
                .WithMany(x => x.TrainingPlans)
                .HasForeignKey(x => x.TrainingCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.TrainingClasses)
                .WithOne(x => x.TrainingPlan)
                .HasForeignKey(x => x.TrainingPlanId)
                .OnDelete(DeleteBehavior.Cascade);


                
        }
    }
}
