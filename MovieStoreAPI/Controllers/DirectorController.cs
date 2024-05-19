using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Context;

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
        public IActionResult GetAllMovies()
        {
            return Ok(_appDbContext.Movie.FirstOrDefault());
        }

        [HttpGet("pregledPojedinogFilma")]
        public IActionResult GetMoviesById(int id)
        {
            return Ok(_appDbContext.Movie.Where(x => x.Id == id).SingleOrDefault());
        }
    }
}
