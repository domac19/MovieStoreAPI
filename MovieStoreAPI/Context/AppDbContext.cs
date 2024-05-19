using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Models;

namespace MovieStoreAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Movie> Movie { get; set; }
    }
}
