using backEnd.Models;

namespace backEnd.Repository
{
    public interface IUserRepository
    {
        User? GetUserByLogin(string login);
        bool AddUser(User user);
        bool Login(string login, string pass);
    }
}
