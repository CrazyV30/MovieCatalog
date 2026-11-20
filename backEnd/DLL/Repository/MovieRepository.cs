using Dll.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dll.Repository
{
    public class MovieRepository(AppDbContext _context) : IRepository<Movie>
    {
        public async Task<bool> Add(Movie entity)
        {
            await _context.Movies.AddAsync(entity);
            return true;
        }

        public async Task<bool> ExistId(int id) => await _context.Movies.AnyAsync(m => m.Id == id);

        public async Task<List<Movie>> GetAll() => await _context.Movies.ToListAsync();

        public async Task<Movie?> GetByName(string name) => await _context.Movies.FirstOrDefaultAsync(m => string.Equals(m.Title.ToLower(), name.ToLower()));
        public async Task<Movie?> GetById(int id) => await _context.Movies.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Id == id);
        public async Task<List<Movie>> SearchBCondition(Expression<Func<Movie, bool>> predicat) => await _context.Movies.Where(predicat).ToListAsync();

        public bool Remove(Movie entity)
        {
            _context.Movies.Remove(entity);
            return true;
        }

        public async Task<bool> Update(Movie entity)
        {
            if (!(await ExistId(entity.Id))) return false;
            var movie = await GetById(entity.Id);
            _context.Entry(movie!).CurrentValues.SetValues(entity);
            return true;
        }
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
