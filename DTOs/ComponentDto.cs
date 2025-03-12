namespace BechsudTestTecnico.DTOs
{
    public class ComponentReadDto
    {
        public int Id { get; set; }
        public string Part { get; set; } = null!;
        public string ComponentType { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public int MachineId { get; set; }
    }

}
