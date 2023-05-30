using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsProjectAPI.Models;

namespace MovieReviewsProjectAPI.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Star>>> GetStars()
        {
          if (_context.Stars == null)
          {
              return NotFound();
          }
            return await _context.Stars.ToListAsync();
        }

        // GET: api/Stars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Star>> GetStar(string id)
        {
          if (_context.Stars == null)
          {
              return NotFound();
          }
            var star = await _context.Stars.FindAsync(id);

            if (star == null)
            {
                return NotFound();
            }

            return star;
        }

        // PUT: api/Stars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStar(string id, Star star)
        {
            if (id != star.Id)
            {
                return BadRequest();
            }

            _context.Entry(star).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StarExists(id))
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

        // POST: api/Stars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Star>> PostStar(Star star)
        {
          if (_context.Stars == null)
          {
              return Problem("Entity set 'AppDbContext.Stars'  is null.");
          }
            _context.Stars.Add(star);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StarExists(star.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStar", new { id = star.Id }, star);
        }

        // DELETE: api/Stars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStar(string id)
        {
            if (_context.Stars == null)
            {
                return NotFound();
            }
            var star = await _context.Stars.FindAsync(id);
            if (star == null)
            {
                return NotFound();
            }

            _context.Stars.Remove(star);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("startswith/{str}")]
        public async Task<ActionResult<List<Star>>> GetStarsByNameStartingWith(string str)
        {
            var stars = await _context.Stars
                .Where(s => s.Name.StartsWith(str))
                .ToListAsync();

            if (stars == null || stars.Count == 0)
            {
                return new List<Star>();
            }

            return stars;
        }

        private bool StarExists(string id)
        {
            return (_context.Stars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
