using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project.Service.Models;

namespace Project.Service.Data
{
    // Interface for the VehicleDbContext
    public interface IVehicleDbContext
    {
        DbSet<VehicleMake> VehicleMakes { get; set; }
        DbSet<VehicleModel> VehicleModels { get; set; }

        Task<int> SaveChangesAsync();
    }

    public class VehicleDbContext : DbContext, IVehicleDbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        // Saves the changes made in the DbContext to the database
        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
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
