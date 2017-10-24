using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Produces("application/json")]
    [Route("api/establishments")]
    public class EstablishmentsController : Controller
    {
        private readonly ApiDBContext _context;

        public EstablishmentsController(ApiDBContext context)
        {
            _context = context;
        }

        // GET: api/Establishments
        [HttpGet]
        public IEnumerable<Establishments> GetEstablishments()
        {
            return _context.Establishments;
        }

        // GET: api/Establishments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstablishments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var establishments = await _context.Establishments.SingleOrDefaultAsync(m => m.EstablishmentID == id);

            if (establishments == null)
            {
                return NotFound();
            }

            return Ok(establishments);
        }

        // PUT: api/Establishments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstablishments([FromRoute] int id, [FromBody] Establishments establishments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != establishments.EstablishmentID)
            {
                return BadRequest();
            }

            _context.Entry(establishments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentsExists(id))
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

        // POST: api/Establishments
        [HttpPost]
        public async Task<IActionResult> PostEstablishments([FromBody] Establishments establishments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Establishments.Add(establishments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstablishments", new { id = establishments.EstablishmentID }, establishments);
        }

        // DELETE: api/Establishments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstablishments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var establishments = await _context.Establishments.SingleOrDefaultAsync(m => m.EstablishmentID == id);
            if (establishments == null)
            {
                return NotFound();
            }

            _context.Establishments.Remove(establishments);
            await _context.SaveChangesAsync();

            return Ok(establishments);
        }

        private bool EstablishmentsExists(int id)
        {
            return _context.Establishments.Any(e => e.EstablishmentID == id);
        }
    }
}