using Ninject;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Ninject.Web.Common.SelfHost;
using Project.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var hostConfiguration = new AspNetCoreHostConfiguration(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .BlockOnStart();

        var host = new NinjectSelfHostBootstrapper(CreateKernel, hostConfiguration);
        host.Start();
    }

    public static IKernel CreateKernel()
    {
        var settings = new NinjectSettings();

        settings.LoadExtensions = false;

        var kernel = new AspNetCoreKernel(settings);

        kernel.Load(typeof(AspNetCoreHostConfiguration).Assembly);

        // Load the Ninject module from the Project.Service project
        kernel.Load(typeof(NinjectBindings).Assembly);

        return kernel;
    }
}