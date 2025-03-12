using System.ComponentModel;

namespace BechsudTestTecnico.Models
{
    public class Machine
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

        //Relación M a M
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}
