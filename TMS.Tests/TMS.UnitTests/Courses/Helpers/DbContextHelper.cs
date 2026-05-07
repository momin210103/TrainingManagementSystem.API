using Microsoft.EntityFrameworkCore;
using TMS.Infrastructure.Persistence;

namespace TMS.UnitTests.Courses.Helpers;

public static class DbContextHelper
{
    public static TmsDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TmsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TmsDbContext(options);
    }
    
}