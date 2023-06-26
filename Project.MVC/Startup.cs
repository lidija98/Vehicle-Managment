using Microsoft.EntityFrameworkCore;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Project.Service;
using Project.Service.Data;
using Project.Service.Services;

public class Startup : AspNetCoreStartupBase
{
    public Startup(IConfiguration configuration, IServiceProviderFactory<NinjectServiceProviderBuilder> providerFactory)
        : base(providerFactory)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        // Add your services configuration HERE

        // Add DbContext using MySQL provider
        services.AddDbContext<VehicleDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 26))));


        // Configure AutoMapper
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddTransient<IVehicleDbContext, VehicleDbContext>();
        services.AddTransient<IVehicleService, VehicleService>();

    }

    public override void Configure(IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

}