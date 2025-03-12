using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BechsudTestTecnico.Data;
using BechsudTestTecnico.Models;
using BechsudTestTecnico.DTOs;

namespace BechsudTestTecnico.Controllers
{
    [ApiController]
    [Route("api/machines/{machineId}/[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly BechsudContext _context;

        public ComponentsController(BechsudContext context)
        {
            _context = context;
        }

        // GET: api/machines/1/components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentReadDto>>> GetComponentsForMachine(int machineId)
        {
            // Verificar si la Machine existe
            var machineExists = await _context.Machines.AnyAsync(m => m.Id == machineId);
            if (!machineExists)
                return NotFound($"Machine with ID={machineId} not found.");

            // Filtrar components por MachineId
            var components = await _context.Components
                .Where(c => c.MachineId == machineId)
                .ToListAsync();

            // Mapear a DTO
            var dtoList = components.Select(c => new ComponentReadDto
            {
                Id = c.Id,
                Part = c.Part,
                ComponentType = c.ComponentType,
                BrandName = c.BrandName,
                Model = c.Model,
                Description = c.Description,
                SerialNumber = c.SerialNumber,
                MachineId = c.MachineId
            }).ToList();

            return dtoList;
        }

        // GET: api/machines/1/components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentReadDto>> GetComponent(int machineId, int id)
        {
            var component = await _context.Components
                .Where(c => c.MachineId == machineId && c.Id == id)
                .FirstOrDefaultAsync();

            if (component == null)
                return NotFound($"Component with ID={id} not found in Machine ID={machineId}.");

            var dto = new ComponentReadDto
            {
                Id = component.Id,
                Part = component.Part,
                ComponentType = component.ComponentType,
                BrandName = component.BrandName,
                Model = component.Model,
                Description = component.Description,
                SerialNumber = component.SerialNumber,
                MachineId = component.MachineId
            };

            return dto;
        }

        // POST: api/machines/1/components
        [HttpPost]
        public async Task<ActionResult<ComponentReadDto>> CreateComponent(int machineId, ComponentCreateDto dto)
        {
            // Verificar que la Machine existe
            var machine = await _context.Machines.FindAsync(machineId);
            if (machine == null)
                return NotFound($"Machine with ID={machineId} not found.");

            // Mapear DTO -> Entidad
            var component = new Component
            {
                Part = dto.Part,
                ComponentType = dto.ComponentType,
                BrandName = dto.BrandName,
                Model = dto.Model,
                Description = dto.Description,
                SerialNumber = dto.SerialNumber,
                MachineId = machineId
            };

            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            // Convertir a ReadDto
            var readDto = new ComponentReadDto
            {
                Id = component.Id,
                Part = component.Part,
                ComponentType = component.ComponentType,
                BrandName = component.BrandName,
                Model = component.Model,
                Description = component.Description,
                SerialNumber = component.SerialNumber,
                MachineId = machineId
            };

            return CreatedAtAction(nameof(GetComponent), new { machineId, id = readDto.Id }, readDto);
        }

        // PUT: api/machines/1/components/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComponent(int machineId, int id, ComponentUpdateDto dto)
        {
            var component = await _context.Components
                .Where(c => c.MachineId == machineId && c.Id == id)
                .FirstOrDefaultAsync();

            if (component == null)
                return NotFound($"Component with ID={id} not found in Machine={machineId}.");

            // Mapear UpdateDto -> Entidad
            component.Part = dto.Part;
            component.ComponentType = dto.ComponentType;
            component.BrandName = dto.BrandName;
            component.Model = dto.Model;
            component.Description = dto.Description;
            component.SerialNumber = dto.SerialNumber;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/machines/1/components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int machineId, int id)
        {
            var component = await _context.Components
                .Where(c => c.MachineId == machineId && c.Id == id)
                .FirstOrDefaultAsync();

            if (component == null)
                return NotFound($"Component with ID={id} not found in Machine={machineId}.");

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
