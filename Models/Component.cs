namespace BechsudTestTecnico.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Part { get; set; } = null!;
        public string ComponentType { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;

        // ForeignKey
        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;
    }
}
