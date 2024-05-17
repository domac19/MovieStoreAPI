using Microsoft.AspNetCore.Mvc;

namespace MovieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        [HttpGet("pregledVlastitihPodataka")]
        public IActionResult GetMyData()
        {
            return Ok();
        }
    }
}
