﻿using Microsoft.EntityFrameworkCore;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Project.MVC.Controllers;
using Project.MVC.MappingProfiles;
using Project.Service;
using Project.Service.Data;
using Project.Service.MappingProfiles;
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

        // Add DbContext using MySQL provider
        services.AddDbContext<VehicleDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 26))));


        // Configure AutoMapper
        services.AddAutoMapper(typeof(ServiceMappingProfile).Assembly, typeof(MvcMappingProfile).Assembly);

        services.AddTransient<IVehicleDbContext, VehicleDbContext>();
        services.AddTransient<IVehicleService, VehicleService>();
        services.AddTransient<IVehicleMakeController, VehicleMakeController>();
        services.AddTransient<IVehicleModelController, VehicleModelController>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }

}