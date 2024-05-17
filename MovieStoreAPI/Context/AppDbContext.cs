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
    }
}
