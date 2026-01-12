using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table 
            builder.ToTable("Roles");
            // Pk
            builder.HasKey(x => x.Id);
            // property
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            // Relationship
            builder.HasMany(x => x.EmployeeRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
