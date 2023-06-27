using Project.Service.DTO;
using X.PagedList;

namespace Project.Service.Services
{
    public interface IVehicleService
    {
        Task<IPagedList<VehicleMakeDto>> GetVehicleMakes(string sort, string filter, int page, int pageSize);
        Task<IPagedList<VehicleModelDto>> GetVehicleModels(string sort, string filter, int page, int pageSize);

		Task<VehicleMakeDto> GetVehicleMake(int id);
		Task<VehicleModelDto> GetVehicleModel(int id);

		Task<int> CreateVehicleMake(VehicleMakeDto vehicleMakeDto);
        Task<int> CreateVehicleModel(VehicleModelDto vehicleModelDto);

		Task<bool> UpdateVehicleMake(VehicleMakeDto vehicleMakeDto);
        Task<bool> UpdateVehicleModel(VehicleModelDto vehicleModelDto);

		Task<bool> DeleteVehicleMake(int id);
		Task<bool> DeleteVehicleModel(int id);

    }
}

