using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dll.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieComment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieComment>().HasOne(c => c.Movie).WithMany(m => m.Comments).HasForeignKey(c => c.MovieId).OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
