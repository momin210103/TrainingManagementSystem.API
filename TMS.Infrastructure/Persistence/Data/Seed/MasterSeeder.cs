namespace TMS.Infrastructure.Persistence.Data.Seed
{
    public class MasterSeeder  
    {
        private readonly IEnumerable<IDataSeeder> _dataSeeders;
        public MasterSeeder(IEnumerable<IDataSeeder> dataSeeders)
        {
            _dataSeeders = dataSeeders;
        }
        public async Task SeedAllAsync()
        {
            foreach (var seeder in _dataSeeders)
            {
                await seeder.SeedAsync();
            }
        }

        
    }
}
