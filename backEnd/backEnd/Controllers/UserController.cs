using backEnd.Contracts;
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
        public ActionResult Login([FromBody]LoginRequest userLogin)
        {
            if (_userService.Login(userLogin.Email, userLogin.Password)) return Ok();
            return BadRequest("Login or Password is uncorrect");
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Register([FromBody] RegisterRequest userRegister)
        {
            var result = _userService.Register(new UserRegisterDTO()
            {
                Email = userRegister.Email,
                Username = userRegister.Username,
                Surname = userRegister.Surname,
                Password = userRegister.Password,
                ConfirmPassword = userRegister.ConfirmPassword
            });
            return Ok();
        }
        [HttpGet("{login}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserResponse> GetUserByLogin([FromRoute] string login)
        {
            var user = _userService.GetUserByLogin(login);
            if (user == null) return NotFound("Not found");
            return Ok(new UserResponse() {
                Email = user.Email,
                Username = user.Username,
                Surname = user.Surname
            });
        }
    }
}
