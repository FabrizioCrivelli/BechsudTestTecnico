using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BechsudTestTecnico.Data;
using BechsudTestTecnico.Models;
using BechsudTestTecnico.DTOs; // Ajusta el namespace a donde colocaste tus DTOs

namespace BechsudTestTecnico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly BechsudContext _context;

        public MachinesController(BechsudContext context)
        {
            _context = context;
        }

        // GET: api/machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineReadDto>>> GetMachines()
        {
            var machines = await _context.Machines.ToListAsync();

            // Mapeamos "Machine" -> "MachineReadDto"
            var machinesDto = machines.Select(m => new MachineReadDto
            {
                Id = m.Id,
                TechnicalLocation = m.TechnicalLocation,
                Description = m.Description,
                Model = m.Model,
                SerialNumber = m.SerialNumber,
                MachineTypeName = m.MachineTypeName,
                BrandName = m.BrandName,
                Criticality = m.Criticality,
                Sector = m.Sector
            }).ToList();

            return Ok(machinesDto);
        }

        // GET: api/machines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MachineReadDto>> GetMachine(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            var machineDto = new MachineReadDto
            {
                Id = machine.Id,
                TechnicalLocation = machine.TechnicalLocation,
                Description = machine.Description,
                Model = machine.Model,
                SerialNumber = machine.SerialNumber,
                MachineTypeName = machine.MachineTypeName,
                BrandName = machine.BrandName,
                Criticality = machine.Criticality,
                Sector = machine.Sector
            };

            return Ok(machineDto);
        }

        // POST: api/machines
        [HttpPost]
        public async Task<ActionResult<MachineReadDto>> CreateMachine(MachineCreateDto dto)
        {
            // Mapeamos "MachineCreateDto" -> "Machine"
            var machine = new Machine
            {
                TechnicalLocation = dto.TechnicalLocation,
                Description = dto.Description,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                MachineTypeName = dto.MachineTypeName,
                BrandName = dto.BrandName,
                Criticality = dto.Criticality,
                Sector = dto.Sector
            };

            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            // Retornar la entidad creada, pero con formato "ReadDto"
            var readDto = new MachineReadDto
            {
                Id = machine.Id,
                TechnicalLocation = machine.TechnicalLocation,
                Description = machine.Description,
                Model = machine.Model,
                SerialNumber = machine.SerialNumber,
                MachineTypeName = machine.MachineTypeName,
                BrandName = machine.BrandName,
                Criticality = machine.Criticality,
                Sector = machine.Sector
            };

            return CreatedAtAction(nameof(GetMachine), new { id = machine.Id }, readDto);
        }

        // PUT: api/machines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, MachineUpdateDto dto)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            // Actualizamos campos del Machine con lo que llega del Dto
            machine.TechnicalLocation = dto.TechnicalLocation;
            machine.Description = dto.Description;
            machine.Model = dto.Model;
            machine.SerialNumber = dto.SerialNumber;
            machine.MachineTypeName = dto.MachineTypeName;
            machine.BrandName = dto.BrandName;
            machine.Criticality = dto.Criticality;
            machine.Sector = dto.Sector;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent(); // 204
        }

        // DELETE: api/machines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MachineExists(int id)
        {
            return _context.Machines.Any(e => e.Id == id);
        }
    }
}
