using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;

namespace Project.MVC.Controllers
{
	public interface IVehicleMakeController
	{
        Task<IActionResult> Index(string sort, string filter, int page = 1);
        IActionResult Create();
        Task<IActionResult> Create(VehicleMakeViewModel viewModel);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, VehicleMakeViewModel viewModel);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);
    }
}

