using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class AttendanceConfiguration: IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            //Table Name
            builder.ToTable("Attendances");
            // Primary Key
            builder.HasKey(x => x.Id);
            // Property
            builder.Property(x => x.SessionDate)
                .IsRequired();
            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.MarkedAt)
                .IsRequired(false);
            // Relation foreign key
            builder.HasOne<Enrollment>()
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
