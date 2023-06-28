using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using AutoMapper;
using Project.Service.DTO;
using Project.MVC.Models;
using X.PagedList;


namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller, IVehicleMakeController
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string sort, string filter, int page = 1)
        {
            const int pageSize = 10;

            var vehicleMakes = await _vehicleService.GetVehicleMakes(sort, filter, page, pageSize);

            var viewModel = _mapper.Map<IPagedList<VehicleMakeViewModel>>(vehicleMakes);

            // Pass the filter and sorting values to the view to maintain them in the input fields
            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSort = sort;

            return View(viewModel);
        }

        // GET: Create // Displays the form for creating a new vehicle make
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create // Creates a new vehicle make
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMakeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleMakeDto = _mapper.Map<VehicleMakeDto>(viewModel);
                await _vehicleService.CreateVehicleMake(vehicleMakeDto);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Edit // Retrieve the details of a specific vehicle make for editing
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleMake = await _vehicleService.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                // Returns 404 Not Found if the vehicle make is not found
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(viewModel);
        }

        // POST: Edit // Updates a vehicle make based on the provided view model data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleMakeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                // Returns 404 Not Found if the id in the URL does not match the id in the view model
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicleMakeDto = _mapper.Map<VehicleMakeDto>(viewModel);
                var success = await _vehicleService.UpdateVehicleMake(vehicleMakeDto);

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
            var vehicleMake = await _vehicleService.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                // Returns 404 Not Found if the vehicle make is not found
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(viewModel);
        }

        // POST: Delete // DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _vehicleService.DeleteVehicleMake(id);
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
