using Project.Service.Services;
using AutoMapper;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.DTO;
using X.PagedList;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleDbContext _dbContext;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // Create a new vehicle make
        public async Task<int> CreateVehicleMake(VehicleMakeDto vehicleMakeDto)
        {
            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);
            _dbContext.VehicleMake.Add(vehicleMake);
            await _dbContext.SaveChangesAsync();

            return vehicleMake.Id;
        }

        // Create a new vehicle model
        public async Task<int> CreateVehicleModel(VehicleModelDto vehicleModelDto)
        {
            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDto);
            _dbContext.VehicleModel.Add(vehicleModel);
            await _dbContext.SaveChangesAsync();

            return vehicleModel.Id;
        }

        // Delete a vehicle make
        public async Task<bool> DeleteVehicleMake(int id)
        {
            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
                return false;

            _dbContext.VehicleMake.Remove(vehicleMake);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Delete a vehicle model
        public async Task<bool> DeleteVehicleModel(int id)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
                return false;

            _dbContext.VehicleModel.Remove(vehicleModel);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Get a vehicle make by Id
        public async Task<VehicleMakeDto> GetVehicleMake(int id)
        {
            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);

            return _mapper.Map<VehicleMakeDto>(vehicleMake);
        }

        // Get all vehicle makes with sorting, filtering, and paging
        public async Task<IPagedList<VehicleMakeDto>> GetVehicleMakes(string sort, string filter, int page, int pageSize)
        {
            var query = _dbContext.VehicleMake.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(m => m.Name != null && m.Name.Contains(filter));
            }

            // Sorting
            switch (sort)
            {
                case "Name_Desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;

                case "Name":
                    query = query.OrderBy(m => m.Name);
                    break;

                default:
                    query = query.OrderBy(m => m.Id);
                    break;
            }

            // Paging
            var pagedMakes = await query.ToPagedListAsync(page, pageSize);

            return _mapper.Map<IPagedList<VehicleMakeDto>>(pagedMakes);
        }

        // Get a vehicle model by Id
        public async Task<VehicleModelDto> GetVehicleModel(int id)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(id);

            return _mapper.Map<VehicleModelDto>(vehicleModel);
        }

        // Get all vehicle models with sorting, filtering, and paging
        public async Task<IPagedList<VehicleModelDto>> GetVehicleModels(string sort, string filter, int page, int pageSize)
        {
            var query = _dbContext.VehicleModel.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(m => m.Name != null && m.Name.Contains(filter));
            }

            // Sorting
            switch (sort)
            {
                case "Name_Desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;

                case "Name":
                    query = query.OrderBy(m => m.Name);
                    break;

                default:
                    query = query.OrderBy(m => m.Id);
                    break;
            }

            // Paging
            var pagedModels = await query.ToPagedListAsync(page, pageSize);

            return _mapper.Map<IPagedList<VehicleModelDto>>(pagedModels);
        }

        // Update a vehicle make
        public async Task<bool> UpdateVehicleMake(VehicleMakeDto vehicleMakeDto)
        {
            var existingVehicleMake = await _dbContext.VehicleMake.FindAsync(vehicleMakeDto.Id);
            if (existingVehicleMake == null)
                return false;

            _mapper.Map(vehicleMakeDto, existingVehicleMake);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Update a vehicle model
        public async Task<bool> UpdateVehicleModel(VehicleModelDto vehicleModelDto)
        {
            var existingVehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModelDto.Id);
            if (existingVehicleModel == null)
                return false;

            _mapper.Map(vehicleModelDto, existingVehicleModel);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
