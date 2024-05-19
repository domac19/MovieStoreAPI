using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Context;
using MovieStoreAPI.Models;

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

        [HttpGet("pregledVlastitihPodataka")]
        public IActionResult GetById(int id)
        {
            var getActorDataById = _appDbContext.Actor.Where(i => i.Id == id).FirstOrDefault();
            
            return Ok(getActorDataById);
        }

        [HttpPut("ažuriranjeVlastitihPodataka")]
        public IActionResult GetMoviesById([FromBody] Actor actor)
        {
            var findActorById = _appDbContext.Actor.Find(actor.Id);

            findActorById!.Name = actor.Name;
            findActorById.Surname = actor.Surname;
            findActorById.Address = actor.Address;
            findActorById.FeePerMovie = actor.FeePerMovie;

            return Ok(actor);
        }
    }
}
