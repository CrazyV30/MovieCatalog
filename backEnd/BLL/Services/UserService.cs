using Bll.DTOs;
using Domain.Models;
using Dll.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Bll.Services
{
    public class UserService(UserRepository _userRepository, IMapper _mapper)
    {
        public async Task<bool> Register(UserRegisterDTO userDTO)
        {
            await _userRepository.AddUser(_mapper.Map<User>(userDTO));
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<UserDTO?> GetUserByLogin(string login) {
            var user = await _userRepository.GetUserByLogin(login);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> Login(string email, string pass) => await _userRepository.Login(email, pass);
    }
}
