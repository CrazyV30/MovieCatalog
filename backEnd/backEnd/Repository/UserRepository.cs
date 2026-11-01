using backEnd.Context;
using backEnd.Models;

namespace backEnd.Repository
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? GetUserByLogin(string login) => _context.Users.FirstOrDefault(u => u.Email == login);

        public bool Login(string login, string pass) => _context.Users.Any<User>(u => string.Equals(u.Email,login) && string.Equals(u.Password, pass));
    }
}
