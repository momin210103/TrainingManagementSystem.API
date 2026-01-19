
using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces.Persistence;

namespace TMS.Infrastructure.Persistence.Data.Seed
{
    public class TrainingClassScheduleSeeder: IDataSeeder
    {
        private readonly ITmsDbContext _context;

        public TrainingClassScheduleSeeder(ITmsDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            

        }
    }
}
