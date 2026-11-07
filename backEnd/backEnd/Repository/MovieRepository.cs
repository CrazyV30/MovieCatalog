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
            _context.Entry<Movie>(GetById(entity.Id)!).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
