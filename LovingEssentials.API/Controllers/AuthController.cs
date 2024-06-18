using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            var user = await _userRepository.Login(login.Email, login.Password);
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if(register == null)
            {
                return BadRequest();
            }
            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = register.Password,
                PhoneNumber = register.PhoneNumber,
                Status = 1,
                Role = BusinessObject.Role.Client
            };
            await _userRepository.Register(user);
            return Ok(user);
        }
    }
}
