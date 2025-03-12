using Microsoft.EntityFrameworkCore;
using BechsudTestTecnico.Models;

namespace BechsudTestTecnico.Data
{
    public class BechsudContext : DbContext
    {
        public BechsudContext(DbContextOptions<BechsudContext> options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Component> Components { get; set; }
    }
}
