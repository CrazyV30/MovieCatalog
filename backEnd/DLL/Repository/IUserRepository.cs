using Domain.Models;

namespace Dll.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByLogin(string login);
        Task<bool> AddUser(User user);
        Task<bool> Login(string login, string pass);
        Task SaveChangesAsync();
    }
}
