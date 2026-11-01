using backEnd.DTOs;
using backEnd.Models;
using backEnd.Repository;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Services
{
    public enum UserRegisterResult
    {
        LoginError,
        PasswordError,
        UserNameError,
        SurnameError,
        Ok
    }
    public class UserService(UserRepository _userRepository)
    {
        public UserRegisterResult Register(UserRegisterDTO userDTO)
        {
            if (userDTO.Email.Length < 7 || userDTO.Email.Length > 50) return UserRegisterResult.LoginError;
            else if (userDTO.Password.Length < 6 || userDTO.Password != userDTO.ConfirmPassword) return UserRegisterResult.PasswordError;
            else if (userDTO.Username.Length < 2 || userDTO.Username.Length>50) return UserRegisterResult.UserNameError;
            else if (userDTO.Surname.Length < 2 || userDTO.Surname.Length>50) return UserRegisterResult.SurnameError;

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

        public User? GetUserByLogin(string login) => _userRepository.GetUserByLogin(login);

        public bool Login(string login, string pass) => _userRepository.Login(login, pass);
    }
}
