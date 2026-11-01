using backEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
