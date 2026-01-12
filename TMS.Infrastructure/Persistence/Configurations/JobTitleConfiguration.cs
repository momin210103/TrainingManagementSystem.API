
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class JobTitleConfiguration: IEntityTypeConfiguration<JobTitle>
    {
        public void Configure(EntityTypeBuilder<JobTitle> builder)
        {
            // Table Name
            builder.ToTable("JobTitles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.HasMany(x => x.Employees)
                .WithOne(x => x.JobTitle)
                .HasForeignKey(x => x.JobTitleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
