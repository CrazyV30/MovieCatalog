using backEnd.Context;
using backEnd.Models;

namespace backEnd.Repository
{
    public class MovieRepository(AppDbContext _context) : IRepository<Movie>
    {
        public bool Add(Movie entity)
        {
            _context.Movies.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool ExistName(string name) => _context.Movies.Any<Movie>(m => string.Equals(m.Title.ToLower(), name.ToLower()));

        public List<Movie> GetAll() => _context.Movies.ToList();

        public Movie? GetByName(string name) => _context.Movies.FirstOrDefault(m => string.Equals(m.Title.ToLower(), name.ToLower()));

        public bool Remove(Movie entity)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
