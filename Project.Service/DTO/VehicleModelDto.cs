namespace Project.Service.DTO
{
    // Represents a data transfer object (DTO) for VehicleModel

    public class VehicleModelDto
	{
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
    }
}

