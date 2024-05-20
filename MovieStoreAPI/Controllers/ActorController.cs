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

        [Authorize]
        [HttpPut("ažuriranjeVlastitihPodataka")]
        public IActionResult UpdateMovies([FromBody] Actor actor)
        {
            var findActorById = _appDbContext.Actor.Find(actor.Id);

            findActorById!.Name = actor.Name;
            findActorById.Surname = actor.Surname;
            findActorById.Address = actor.Address;
            findActorById.FeePerMovie = actor.FeePerMovie;

            return Ok(actor);
        }

        [HttpGet("pregledFilmovaSPozivnicom")]
        public IActionResult GetInvitedMovies()
        {
            var invitedMovies = _appDbContext.Movie.Where(m => m.Invitation == "invited").ToList();

            return Ok(invitedMovies);
        }

        [HttpGet("pregledGdjeJeGlumacPrijava")]
        public IActionResult GetAppliedMovies()
        {
            var appliedMovies = _appDbContext.Movie.Where(m => m.ActorApplication == "applied").ToList();

            return Ok(appliedMovies);
        }

        [HttpGet("pregledFilmovaGdjeJeUloga")]
        public IActionResult GetSecuredMovies()
        {
            var securedMovies = _appDbContext.Movie.Where(m => m.Role == "secured").ToList();

            return Ok(securedMovies);
        }

        [HttpGet("filtriranjeSvihSlobodnihTermina")]
        public IActionResult GetAvailableSlots()
        {
            var availableSlots = _appDbContext.Slot.Where(s => !s.IsBooked).ToList();

            return Ok(availableSlots);
        }

        [HttpGet("filtriranjePoDirektoru")]
        public IActionResult GetMoviesByDirector(string director, [FromQuery] string? genre, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = _appDbContext.Movie
                .Where(m => m.Director == director)
                .AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }

            if (startDate.HasValue)
            {
                query = query.Where(m => m.StartOfFilming >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(m => m.EndOfFilming <= endDate.Value);
            }

            var movies = query.ToList();
            return Ok(movies);
        }

        [HttpGet("filtriranjePremaZanruDatumu")]
        public IActionResult GetMoviesByGenre(string genre, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = _appDbContext.Movie.Where(m => m.Genre == genre).AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(m => m.StartOfFilming >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(m => m.EndOfFilming <= endDate.Value);
            }

            var movies = query.ToList();
            return Ok(movies);
        }
    }
}
