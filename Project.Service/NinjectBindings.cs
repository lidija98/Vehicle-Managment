using Project.Service.Data;
using Project.Service.Services;

namespace Project.Service
{
	public class NinjectBindings : Ninject.Modules.NinjectModule
	{
        public override void Load()
        {
            Bind<IVehicleDbContext>().To<VehicleDbContext>();
            Bind<IVehicleService>().To<VehicleService>();
        }
    }
}

