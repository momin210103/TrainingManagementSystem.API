using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class TrainingClassScheduleConfiguration: IEntityTypeConfiguration<TrainingClassSchedule>
    {
        public void Configure(EntityTypeBuilder<TrainingClassSchedule> builder)
        {
            // Table name
            builder.ToTable("TrainingClassSchedules");
            //Pk
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.SessionDate)
                .IsRequired();
            builder.Property(x => x.StartTime)
                .IsRequired();
            builder.Property(x => x.EndTime)
                .IsRequired();
            // Relationship
            builder.HasOne(x => x.TrainingClass)
                .WithMany(x => x.TrainingClassSchedules)
                .HasForeignKey(x => x.TrainingClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
