using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project.Service.Models;

namespace Project.Service.Data
{
    // Interface for the VehicleDbContext
    public interface IVehicleDbContext
    {
        DbSet<VehicleMake> VehicleMake { get; set; }
        DbSet<VehicleModel> VehicleModel { get; set; }

        Task<int> SaveChangesAsync();
    }

    public class VehicleDbContext : DbContext, IVehicleDbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

        // Saves the changes made in the DbContext to the database
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VehicleDbContext>
    {
        public VehicleDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Project.MVC/appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<VehicleDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

            builder.UseMySql(connectionString, serverVersion);
            return new VehicleDbContext(builder.Options);
        }
    }
}
