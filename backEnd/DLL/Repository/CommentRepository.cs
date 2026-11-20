using Dll.Context;
using Dll.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repository
{
    public class CommentRepository(AppDbContext _context) : IRepository<MovieComment>
    {
        public async Task<bool> Add(MovieComment entity)
        {
            entity.Movie = (await _context.Movies.FirstOrDefaultAsync(m => m.Id == entity.MovieId))!;
            await _context.Comments.AddAsync(entity);
            return true;
        }

        public async Task<bool> ExistId(int id) => await _context.Comments.AnyAsync<MovieComment>(c => c.Id == id);

        public async Task<List<MovieComment>> GetAll() => await _context.Comments.ToListAsync();

        public async Task<MovieComment?> GetById(int id) => await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

        public bool Remove(MovieComment entity)
        {
            _context.Comments.Remove(entity);
            return true;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<bool> Update(MovieComment entity)
        {
            var com = await _context.Comments.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (com == null) return false;
            com.Text = entity.Text;
            return true;
        }
    }
}
