﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Context;
using MovieStoreAPI.Models;

namespace MovieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public DirectorController(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        [HttpGet("pregledSvihFilmova")]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            return Ok(_appDbContext.Movie.ToList());
        }

        [HttpGet("pregledPojedinogFilma")]
        public IActionResult GetMoviesById(int id)
        {
            return Ok(_appDbContext.Movie.Where(x => x.Id == id).SingleOrDefault());
        }

        [Authorize]
        [HttpPost("kreiranjeFilma")]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return NotFound();
            }

            _appDbContext.Movie.Add(movie);
            _appDbContext.SaveChanges();

            return Ok(movie);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            if (movie?.Id == null)            
                return NotFound();
            
            var getById = _appDbContext.Movie.Find(movie.Id);

            if (movie == null)
                return NotFound();
            
            getById!.Title = movie.Title;
            getById.StartOfFilming = movie.StartOfFilming;
            getById.EndOfFilming = movie.EndOfFilming;
            getById.Description = movie.Description;
            getById.Budget = movie.Budget;
            getById.Genre = movie.Genre;

            _appDbContext.SaveChanges();
            
            return Ok(movie);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteMovieById(int id)
        {
            var getId = _appDbContext.Movie.Where(x => x.Id == id).FirstOrDefault();

            if (getId == null)
                return NotFound();

            _appDbContext.Movie.Remove(getId);
            _appDbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("pregledSvihGlumacaZaPojediniFilm")]
        public IActionResult GetActorsByMovie(int id)
        {
            var movie = _appDbContext.Movie.Include(m => m.Actors).FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var actors = movie.Actors;
            return Ok(actors);
        }

        [HttpGet("filtriranjeFilma")]
        public IActionResult FilterMovies([FromQuery] string? genre, [FromQuery] decimal? budget, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (genre == null && budget == null && startDate == null && endDate == null)
            {
                return NotFound();
            }

            var query = _appDbContext.Movie.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }

            if (budget.HasValue)
            {
                query = query.Where(m => m.Budget <= budget.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(m => m.StartOfFilming >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(m => m.EndOfFilming <= endDate.Value);
            }

            var filteredMovies = query.ToList();
            return Ok(filteredMovies);
        }
    }
}
