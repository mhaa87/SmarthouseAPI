using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHouseAPI.Models;

namespace SmartHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly LightContext _context;

        public LightsController(LightContext context)
        {
            _context = context;
        }

        // GET: api/Lights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Light>>> GetLights()
        {
            return await _context.Lights.ToListAsync();
        }

        // GET: api/Lights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Light>> GetLight(long id)
        {
            var light = await _context.Lights.FindAsync(id);

            if (light == null)
            {
                return NotFound();
            }

            return light;
        }

        // PUT: api/Lights/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLight(long id, Light light)
        {
            if (id != light.Id)
            {
                return BadRequest();
            }

            _context.Entry(light).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lights
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Light>> PostLight(Light light)
        {
            _context.Lights.Add(light);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLight", new { id = light.Id }, light);
        }

        // DELETE: api/Lights/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Light>> DeleteLight(long id)
        {
            var light = await _context.Lights.FindAsync(id);
            if (light == null)
            {
                return NotFound();
            }

            _context.Lights.Remove(light);
            await _context.SaveChangesAsync();

            return light;
        }

        private bool LightExists(long id)
        {
            return _context.Lights.Any(e => e.Id == id);
        }
    }
}
