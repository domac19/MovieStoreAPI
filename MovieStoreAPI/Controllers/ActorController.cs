using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Context;

namespace MovieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        
        public ActorController(AppDbContext appDbContext)
        {
             _appDbContext = appDbContext;
        }

        [Authorize]
        [HttpGet("pregledVlastitihPodataka")]
        public IActionResult GetAllMovies()
        {
            return Ok(_appDbContext.Movie.FirstOrDefault());
        }

        [HttpGet("id")]
        public IActionResult GetMoviesById(int id)
        {
            return Ok(_appDbContext.Movie.Where(x => x.Id == id).SingleOrDefault());
        }
    }
}
