using Bll.DTOs;
using Domain.Models;
using Dll.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Bll.Services
{
    public enum UserRegisterResult
    {
        Ok
    }
    public class UserService(UserRepository _userRepository, IMapper _mapper)
    {
        public UserRegisterResult Register(UserRegisterDTO userDTO)
        {
            _userRepository.AddUser(_mapper.Map<User>(userDTO));
            return UserRegisterResult.Ok;
        }

        public UserDTO? GetUserByLogin(string login) {
            var user = _userRepository.GetUserByLogin(login);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);
        }

        public bool Login(string email, string pass) => _userRepository.Login(email, pass);
    }
}
