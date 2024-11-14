using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFlowers.Data;
using MvcFlowers.Models;

namespace MvcFlowers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonoFlowersController : ControllerBase
    {
        private readonly MvcFlowersContext _context;

        public MonoFlowersController(MvcFlowersContext context)
        {
            _context = context;
        }

        // GET: api/MonoFlowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonoFlowers>>> GetMonoFlowers()
        {
            return Ok(await _context.MonoFlowers.ToListAsync());
        }

        // GET: api/MonoFlowers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonoFlowers>> GetMonoFlower(int id)
        {
            var monoFlowers = await _context.MonoFlowers.FindAsync(id);

            if (monoFlowers == null)
            {
                return NotFound();
            }

            return Ok(monoFlowers);
        }

        // POST: api/MonoFlowers
        [HttpPost]
        public async Task<ActionResult<MonoFlowers>> CreateMonoFlower([FromBody] MonoFlowers monoFlowers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monoFlowers);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMonoFlower), new { id = monoFlowers.MonoFlowerId }, monoFlowers);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/MonoFlowers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMonoFlower(int id, [FromBody] MonoFlowers monoFlowers)
        {
            if (id != monoFlowers.MonoFlowerId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monoFlowers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonoFlowersExists(monoFlowers.MonoFlowerId))
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

            return BadRequest(ModelState);
        }

        // DELETE: api/MonoFlowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonoFlower(int id)
        {
            var monoFlowers = await _context.MonoFlowers.FindAsync(id);
            if (monoFlowers == null)
            {
                return NotFound();
            }

            _context.MonoFlowers.Remove(monoFlowers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonoFlowersExists(int id)
        {
            return _context.MonoFlowers.Any(e => e.MonoFlowerId == id);
        }
    }
}
