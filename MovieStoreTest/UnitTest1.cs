using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Context;
using MovieStoreAPI.Controllers;
using MovieStoreAPI.Models;

namespace MovieStoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllMovies_WithOkResult()
        {
            // Arrange - nekakve postavke za db context i slièno
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieStore")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Movie.AddRange(
                    new Movie
                    {
                        Id = 1,
                        Title = "Movie 1",
                        Director = "Director 1",
                        Description = "Description 1",
                        Duration = "120",
                        Genre = "Genre 1",
                        Invitation = "true",
                        Role = "Role 1",
                        ActorApplication = "ActorApplication 1"
                    },
                    new Movie
                    {
                        Id = 2,
                        Title = "Movie 2",
                        Director = "Director 2",
                        Description = "Description 2",
                        Duration = "150",
                        Genre = "Genre 2",
                        Invitation = "false",
                        Role = "Role 2",
                        ActorApplication = "ActorApplication 2"
                    }
                );
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new DirectorController(context);

                // Act
                var result = controller.GetAllMovies();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnValue = Assert.IsType<List<Movie>>(okResult.Value);
                Assert.Equal(2, returnValue.Count);
            }
        }
    }
}