using Dll.Context;
using Dll.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repository
{
    public class SessionRepository(AppDbContext _context) : IRepository<MovieSession>
    {
        public Task<bool> Add(MovieSession entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistId(int id) => await _context.Sessions.AnyAsync(s=>s.Id==id);

        public async Task<List<MovieSession>> GetAll() => await _context.Sessions.ToListAsync();

        public async Task<MovieSession?> GetById(int id) => await _context.Sessions.FirstOrDefaultAsync(s=>s.Id==id);

        public bool Remove(MovieSession entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public Task<bool> Update(MovieSession entity)
        {
            throw new NotImplementedException();
        }
    }
}
