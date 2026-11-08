using Bll.DTOs;
using Domain.Models;
using Dll.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bll.Services
{
    public enum UserRegisterResult
    {
        Ok
    }
    public class UserService(UserRepository _userRepository)
    {
        public UserRegisterResult Register(UserRegisterDTO userDTO)
        {
            var user = new User()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Username = userDTO.Username,
                Surname = userDTO.Surname
            };
            _userRepository.AddUser(user);
            return UserRegisterResult.Ok;
        }

        public UserDTO? GetUserByLogin(string login) {
            var user = _userRepository.GetUserByLogin(login);
            if (user == null) return null;
            return new UserDTO() { Email = user.Email, Surname = user.Surname, Username = user.Username };
        }

        public bool Login(string login, string pass) => _userRepository.Login(login, pass);
    }
}
