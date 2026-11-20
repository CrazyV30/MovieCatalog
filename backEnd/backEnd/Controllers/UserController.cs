using AutoMapper;
using backEnd.Contracts;
using Bll.DTOs;
using Bll.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService _userService, IValidator<RegisterRequest> _validator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login([FromBody]LoginRequest userLogin)
        {
            if (await _userService.Login(userLogin.Email, userLogin.Password)) return Ok();
            return BadRequest("Login or Password is uncorrect");
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterRequest userRegister)
        {
            var res = _validator.Validate(userRegister);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _userService.Register(_mapper.Map<UserRegisterDTO>(userRegister));
            return Ok();
        }
        [HttpGet("{login}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponse>> GetUserByLogin([FromRoute] string login)
        {
            var user = await _userService.GetUserByLogin(login);
            if (user == null) return NotFound("Not found");
            return Ok(_mapper.Map<UserResponse>(user));
        }
    }
}
