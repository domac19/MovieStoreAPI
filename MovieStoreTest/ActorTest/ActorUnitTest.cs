using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Context;
using MovieStoreAPI.Controllers;
using MovieStoreAPI.Models;

namespace MovieStoreTest.ActorTest
{
    public class ActorUnitTest
    {
        [Fact]
        public void GetActorById_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieStore")
                .Options;

            var context = new AppDbContext(options);

            var movie1 = new Movie
            {
                Id = 27,
                Title = "Movie 1",
                Director = "Director 1",
                Description = "Description 1",
                Duration = "120",
                Genre = "Genre 1",
                Invitation = "true",
                Role = "Role 1",
                ActorApplication = "ActorApplication 1"
            };

            var actor = new Actor { Id = 22, Name = "Pero", Surname = "Perić", Address = "Neka Ulica 21", FeePerMovie = "123", Movies = new List<Movie>{ movie1 } };

            context.AddRange(actor);

            context.SaveChanges();

            var controller = new ActorController(context);

            var result = controller.GetById(22);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
