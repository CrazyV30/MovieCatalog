using backEnd.DTOs;
using backEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService _userService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login([FromBody]UserLoginDTO userLogin)
        {
            if (_userService.Login(userLogin.Email, userLogin.Password)) return Ok();
            return BadRequest("Login or Password is uncorrect");
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Register([FromBody] UserRegisterDTO userRegister)
        {
            var result = _userService.Register(userRegister);
            switch (result)
            {
                case UserRegisterResult.LoginError: return BadRequest("Uncorrect Login");
                case UserRegisterResult.PasswordError: return BadRequest("Uncorrect Password");
                case UserRegisterResult.UserNameError: return BadRequest("Uncorrect Username");
                case UserRegisterResult.SurnameError: return BadRequest("Uncorrect Surname");
                default: return Ok();
            }
        }
    }
}
