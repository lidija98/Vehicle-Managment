using AutoMapper;
using Project.MVC.Models;
using Project.Service.DTO;

namespace Project.MVC.MappingProfiles
{
	public class MvcMappingProfile : Profile
	{
		public MvcMappingProfile()
		{
			CreateMap<VehicleMakeDto, VehicleMakeViewModel>();
			CreateMap<VehicleMakeViewModel, VehicleMakeDto>();

			CreateMap<VehicleModelDto, VehicleModelViewModel>();
			CreateMap<VehicleModelViewModel, VehicleModelDto>();
		}
	}
}

