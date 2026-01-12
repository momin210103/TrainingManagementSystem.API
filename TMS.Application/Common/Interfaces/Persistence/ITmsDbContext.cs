using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces.Persistence
{
    public interface ITmsDbContext
    {
         DbSet<Employee> Employees { get; }
         DbSet<Department> Departments { get; }
         DbSet<JobTitle> JobTitles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
