using AutoMapper;
using Project.Service.Models;
using Project.Service.DTO;

namespace Project.Service.MappingProfiles
{
	public class ServiceMappingProfile : Profile
	{
        public ServiceMappingProfile()
		{
			CreateMap<VehicleMake, VehicleMakeDto>();
			CreateMap<VehicleMakeDto, VehicleMake>();

			CreateMap<VehicleModel, VehicleModelDto>();
			CreateMap<VehicleModelDto, VehicleModel>();
		}
	}
}

