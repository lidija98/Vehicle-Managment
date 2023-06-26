using Project.Service.DTO;

namespace Project.Service.Services
{
    public interface IVehicleService
    {
		Task<List<VehicleMakeDto>> GetVehicleMakes();
		Task<List<VehicleModelDto>> GetVehicleModels();

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

