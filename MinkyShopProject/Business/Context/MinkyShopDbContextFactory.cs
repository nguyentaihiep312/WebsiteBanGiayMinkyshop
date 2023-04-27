using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MinkyShopProject.Business.Context
{
    public class MinkyShopDbContextFactory : IDesignTimeDbContextFactory<MinkyShopDbContext>
    {
        public MinkyShopDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<MinkyShopDbContext>().UseSqlServer(connectionString);
            return new MinkyShopDbContext(optionBuilder.Options);
        }
    }
}
