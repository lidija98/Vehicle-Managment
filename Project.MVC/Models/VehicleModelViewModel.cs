using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.MVC.Models;

public class VehicleModelViewModel
{
    public int Id { get; set; }
    public int MakeId { get; set; }
    public string? Name { get; set; }
    public string? Abrv { get; set; }

    // Property to hold the selected make's Id
    public int SelectedMakeId { get; set; }

    public List<SelectListItem>? AvailableMakes { get; set; }

}
