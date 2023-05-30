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
    public class MovieStarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieStarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieStars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieStar>>> GetMovieStar()
        {
          if (_context.MovieStar == null)
          {
              return NotFound();
          }
            return await _context.MovieStar.ToListAsync();
        }

        // GET: api/MovieStars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieStar>> GetMovieStar(string id)
        {
          if (_context.MovieStar == null)
          {
              return NotFound();
          }
            var movieStar = await _context.MovieStar.FindAsync(id);

            if (movieStar == null)
            {
                return NotFound();
            }

            return movieStar;
        }

        // PUT: api/MovieStars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieStar(string id, MovieStar movieStar)
        {
            if (id != movieStar.MovieStarId)
            {
                return BadRequest();
            }

            _context.Entry(movieStar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieStarExists(id))
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

        // POST: api/MovieStars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieStar>> PostMovieStar(MovieStar movieStar)
        {
          if (_context.MovieStar == null)
          {
              return Problem("Entity set 'AppDbContext.MovieStar'  is null.");
          }
            _context.MovieStar.Add(movieStar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovieStarExists(movieStar.MovieStarId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovieStar", new { id = movieStar.MovieStarId }, movieStar);
        }

        // DELETE: api/MovieStars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieStar(string id)
        {
            if (_context.MovieStar == null)
            {
                return NotFound();
            }
            var movieStar = await _context.MovieStar.FindAsync(id);
            if (movieStar == null)
            {
                return NotFound();
            }

            _context.MovieStar.Remove(movieStar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieStarExists(string id)
        {
            return (_context.MovieStar?.Any(e => e.MovieStarId == id)).GetValueOrDefault();
        }

        // GET: api/MovieStars/GetMovieStarsByMovieId/{movieId}
        [HttpGet("GetMovieStarsByMovieId/{movieId}")]
        public async Task<ActionResult<IEnumerable<MovieStar>>> GetMovieStarsByMovieId(string movieId)
        {
            var movieStars = await _context.MovieStar.Where(ms => ms.MovieId == movieId).ToListAsync();

            if (movieStars == null || movieStars.Count == 0)
            {
                return new List<MovieStar>(); // Return an empty list
            }

            return movieStars;
        }

        // DELETE: api/MovieStars/DeleteByMovieId/{movieId}
        [HttpDelete("DeleteByMovieId/{movieId}")]
        public async Task<IActionResult> DeleteByMovieId(string movieId)
        {
            var movieStarsToDelete = await _context.MovieStar.Where(ms => ms.MovieId == movieId).ToListAsync();

            if (movieStarsToDelete == null || movieStarsToDelete.Count == 0)
            {
                return NotFound();
            }

            _context.MovieStar.RemoveRange(movieStarsToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MovieStars/DeleteByStarId/{starId}
        [HttpDelete("DeleteByStarId/{starId}")]
        public async Task<IActionResult> DeleteByStarId(string starId)
        {
            var movieStarsToDelete = await _context.MovieStar.Where(ms => ms.StarId == starId).ToListAsync();

            if (movieStarsToDelete == null || movieStarsToDelete.Count == 0)
            {
                return NotFound();
            }

            _context.MovieStar.RemoveRange(movieStarsToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
