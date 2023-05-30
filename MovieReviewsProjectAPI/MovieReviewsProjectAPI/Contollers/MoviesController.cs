using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsProjectAPI.Models;
using NuGet.Protocol;

namespace MovieReviewsProjectAPI.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(string id)
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(string id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
          if (_context.Movies == null)
          {
              return Problem("Entity set 'AppDbContext.Movies'  is null.");
          }
            _context.Movies.Add(movie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovieExists(movie.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(string id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("startswith/{str}")]
        public async Task<ActionResult<List<Movie>>> GetMoviesByNameStartingWith(string str)
        {
            var movies = await _context.Movies
                .Where(s => s.Title.StartsWith(str))
                .ToListAsync();

            if (movies == null || movies.Count == 0)
            {
                return new List<Movie>();
            }

            return movies;
        }

        // PUT: api/Movies/UpdateMoviesByDirector/{directorId}
        [HttpPut("UpdateMoviesByDeletedDirector/{directorId}")]
        public async Task<bool> UpdateMoviesByDeletedDirector(string directorId)
        {
            try
            {
                var moviesToUpdate = await _context.Movies.Where(m => m.Director_Id == directorId).ToListAsync();

                foreach (var movie in moviesToUpdate)
                {
                    movie.Director_Id = string.Empty;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle the exception or log the error
                return false;
            }
        }

        // PUT: api/Movies/UpdateMoviesByDirector/{directorId}
        [HttpPut("UpdateMoviesByDeletedCompany/{company}")]
        public async Task<bool> UpdateMoviesByDeletedCompany(string company)
        {
            try
            {
                var moviesToUpdate = await _context.Movies.Where(m => m.Company == company).ToListAsync();

                foreach (var movie in moviesToUpdate)
                {
                    movie.Company = string.Empty;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle the exception or log the error
                return false;
            }
        }

        // PUT: api/Movies/UpdateMoviesByCompany
        [HttpPut("UpdateMoviesByCompany")]
        public async Task<IActionResult> UpdateMoviesByCompany([FromBody] CompanyUpdateRequest request)
        {
            try
            {
                var moviesToUpdate = await _context.Movies
                    .Where(m => m.Company.Contains(request.OldCompanyName))
                    .ToListAsync();

                foreach (var movie in moviesToUpdate)
                {
                    movie.Company = movie.Company.Replace(request.OldCompanyName, request.NewCompanyName);
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                // Handle the exception or log the error
                return StatusCode(500, "An error occurred while updating movies by company.");
            }
        }




    }
}
