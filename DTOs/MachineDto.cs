namespace BechsudTestTecnico.DTOs
{
    public class MachineReadDto
    {
        public int Id { get; set; }
        public string TechnicalLocation { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string MachineTypeName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string Criticality { get; set; } = null!;
        public string Sector { get; set; } = null!;
    }
}
