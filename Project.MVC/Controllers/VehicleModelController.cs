using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using AutoMapper;
using Project.Service.DTO;
using Project.MVC.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller, IVehicleModelController
    {

        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string sort, string filter,int page = 1)
        {
            const int pageSize = 10;

            var vehicleModels = await _vehicleService.GetVehicleModels(sort, filter, page, pageSize);

            var viewModel = _mapper.Map<IPagedList<VehicleModelViewModel>>(vehicleModels);

            // Pass the filter and sorting values to the view to maintain them in the input fields
            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSort = sort;

            return View(viewModel);
        }

        // GET: Create // Displays the form for creating a new vehicle make
        public async Task<IActionResult> Create()
        {
            var viewModel = new VehicleModelViewModel();

            // Fetch the list of available makes from your service or repository
            var makes = await _vehicleService.GetVehicleMakes(null, null, 1, int.MaxValue);

            viewModel.AvailableMakes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(viewModel);
        }

        // POST: Create // Creates a new vehicle make
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected make's Id from the view model and assign it to the VehicleModelDto
                viewModel.MakeId = viewModel.SelectedMakeId;

                var vehicleModelDto = _mapper.Map<VehicleModelDto>(viewModel);

                await _vehicleService.CreateVehicleModel(vehicleModelDto);

                return RedirectToAction(nameof(Index));
            }

            // Re-populate the available makes in case of validation errors
            var makes = await _vehicleService.GetVehicleMakes(null, null, 1, int.MaxValue);
            viewModel.AvailableMakes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(viewModel);
        }

        // GET: Edit // Retrieve the details of a specific vehicle make for editing
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleModel = await _vehicleService.GetVehicleModel(id);
            if (vehicleModel == null)
            {
                // Returns 404 Not Found if the vehicle make is not found
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(viewModel);
        }

        // POST: Edit // Updates a vehicle make based on the provided view model data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModelViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                // Returns 404 Not Found if the id in the URL does not match the id in the view model
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicleModelDto = _mapper.Map<VehicleModelDto>(viewModel);

                var success = await _vehicleService.UpdateVehicleModel(vehicleModelDto);

                if (!success)
                {
                    // Returns 404 Not Found if the update operation fails
                    return NotFound();
                }

                // Returns 302 Found (redirect) to the Index action after a successful update
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Delete // Displays the delete confirmation page for a specific vehicle make
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleModel = await _vehicleService.GetVehicleModel(id);
            if (vehicleModel == null)
            {
                // Returns 404 Not Found if the vehicle make is not found
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(viewModel);
        }

        // POST: Delete // DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _vehicleService.DeleteVehicleModel(id);
            if (!success)
            {
                // Returns 404 Not Found if the delete operation fails
                return NotFound();
            }

            // Returns 404 Not Found if the delete operation fails
            return RedirectToAction(nameof(Index));
        }
    }
}