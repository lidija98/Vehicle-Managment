using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;

namespace Project.MVC.Controllers
{
    public interface IVehicleModelController
	{
        Task<IActionResult> Index(string sort, string filter, int page = 1);
        Task<IActionResult> Create();
        Task<IActionResult> Create(VehicleModelViewModel viewModel);
        Task<IActionResult> Edit(int id);
        Task<IActionResult> Edit(int id, VehicleModelViewModel viewModel);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> DeleteConfirmed(int id);
    }
}

