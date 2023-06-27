namespace Project.MVC.Models;

public class VehicleModelViewModel
{
    public int Id { get; set; }
    public int MakeId { get; set; }
    public string? Name { get; set; }
    public string? Abrv { get; set; }
    public string? MakeName { get; set; }

    // Example of a calculated property
    public string DisplayText => $"{MakeName} - {Name} ({Abrv})";
}
