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

        public bool ExistId(int id) => _context.Movies.Any(m => m.Id == id);

        public List<Movie> GetAll() => _context.Movies.ToList();

        public Movie? GetByName(string name) => _context.Movies.FirstOrDefault(m => string.Equals(m.Title.ToLower(), name.ToLower()));
        public Movie? GetById(int id) => _context.Movies.FirstOrDefault(m => m.Id == id);

        public bool Remove(Movie entity)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Movie entity)
        {
            if (!ExistId(entity.Id)) return false;;
            var movie = _context.Movies.FirstOrDefault(m => entity.Id == m.Id);
            movie.Title = entity.Title;
            movie.Description = entity.Description;
            movie.PhotoPath = entity.PhotoPath;
            movie.Director = entity.Director;
            movie.Runtime = entity.Runtime;
            movie.ReleaseYear = entity.ReleaseYear;
            _context.SaveChanges();
            return true;
        }
    }
}
