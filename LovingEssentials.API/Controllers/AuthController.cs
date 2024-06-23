using Azure.Core;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            var user = await _userRepository.Login(login.Email); // Chỉ truyền email
            if (user == null || !VerifyPassword(user, login.Password))
            {
                return Unauthorized(new AuthResponse { Success = false, Errors = new List<string> { "Invalid username or password" } });
            }
            var token = GenerateJwtToken(user);
            return Ok(new { user, token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (register == null)
            {
                return BadRequest();
            }

            if (await _userRepository.UserExistsByEmail(register.Email))
            {
                return BadRequest("User already exists");
            }

            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = HashPassword(new User(), register.Password), // Hash password
                PhoneNumber = register.PhoneNumber,
                Status = 1,
                Role = BusinessObject.Role.Client
            };

            await _userRepository.Register(user);

            var token = GenerateJwtToken(user); // Generate token
            return Ok(new { user, token });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(User account, string password)
        {
            return _passwordHasher.HashPassword(account, password);
        }

        private bool VerifyPassword(User account, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(account, account.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}