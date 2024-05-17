using Microsoft.AspNetCore.Mvc;

namespace MovieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        [HttpGet("pregledSvihFilmova")]
        public IActionResult GetAllMovies()
        {
            return Ok();
        }
    }
}
