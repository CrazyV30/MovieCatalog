using Microsoft.EntityFrameworkCore;

namespace backEnd.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
