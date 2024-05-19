using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
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

        [HttpPost("kreiranjeFilma")]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            _appDbContext.Movie.Add(movie);
            _appDbContext.SaveChanges();

            return Ok(movie);
        }

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
    }
}
