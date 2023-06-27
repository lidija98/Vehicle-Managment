namespace Project.MVC.Models;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Abrv { get; set; }

    // Example of a calculated property
    public string FullName => $"{Name} ({Abrv})";
}
