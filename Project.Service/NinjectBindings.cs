using Project.Service.Data;

namespace Project.Service
{
	public class NinjectBindings : Ninject.Modules.NinjectModule
	{
        public override void Load()
        {
            Bind<IVehicleDbContext>().To<VehicleDbContext>();
        }
    }
}

