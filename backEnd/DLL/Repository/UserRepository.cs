using Dll.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dll.Repository
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return true;
        }

        public async Task<User?> GetUserByLogin(string login) => await _context.Users.FirstOrDefaultAsync(u => u.Email == login);

        public async Task<bool> Login(string email, string pass) => await _context.Users.AnyAsync<User>(u => string.Equals(u.Email,email) && string.Equals(u.Password, pass));

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
