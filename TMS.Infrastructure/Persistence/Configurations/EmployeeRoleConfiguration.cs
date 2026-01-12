using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class EmployeeRoleConfiguration: IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            //Table Name
            builder.ToTable("EmployeeRoles");
            // Composite Pk
            builder.HasKey(x => new { x.EmployeeId, x.RoleId });
            // Property
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeRoles)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Role)
                .WithMany(x => x.EmployeeRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
