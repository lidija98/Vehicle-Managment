using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.DTO;
using Project.MVC.Models;


namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }


        // GET: Retrieves a list of vehicle makes
        public async Task<IActionResult> Index()
        {
            var vehicleMakes = await _vehicleService.GetVehicleMakes();
            var viewModel = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            return View(viewModel);
        }

        // GET: Create // Displays the form for creating a new vehicle make
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMakeViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var vehicleMakeDto = _mapper.Map<VehicleMakeDto>(viewModel);
                await _vehicleService.CreateVehicleMake(vehicleMakeDto);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}

