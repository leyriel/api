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
    [Route("api/Institutes")]
    public class InstitutesController : Controller
    {
        private readonly ApiDBContext _context;

        public InstitutesController(ApiDBContext context)
        {
            _context = context;
        }

        // GET: api/Institutes
        [HttpGet]
        public IEnumerable<Institute> GetInstitutes()
        {
            return _context.Institutes;
        }

        // GET: api/Institutes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institute = await _context.Institutes.SingleOrDefaultAsync(m => m.InstituteID == id);                

            if (institute == null)
            {
                return NotFound();
            }

            return Ok(institute);
        }

        // PUT: api/Institutes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute([FromRoute] int id, [FromBody] Institute institute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != institute.InstituteID)
            {
                return BadRequest();
            }

            _context.Entry(institute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituteExists(id))
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

        // POST: api/Institutes
        [HttpPost]
        public async Task<IActionResult> PostInstitute([FromBody] Institute institute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Institutes.Add(institute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitute", new { id = institute.InstituteID }, institute);
        }

        // DELETE: api/Institutes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institute = await _context.Institutes.SingleOrDefaultAsync(m => m.InstituteID == id);
            if (institute == null)
            {
                return NotFound();
            }

            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();

            return Ok(institute);
        }

        private bool InstituteExists(int id)
        {
            return _context.Institutes.Any(e => e.InstituteID == id);
        }
    }
}