using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepository.ListAllUser());
        }
        [HttpGet("user-detail/{id}")]
        public async Task<IActionResult> GetDetail([FromRoute]int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("new-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserCRUD user)
        {
            var u = new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status
            };

            await _userRepository.CreateUser(u);
            return Ok("Created Successfully");
        }

        [HttpPut("udpate-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserCRUD user)
        {
            var u = await _userRepository.GetUserById(user.Id);
            if (u != null)
            {
                u.Name = user.Name;
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                u.Password = user.Password;
                u.Status = user.Status;

                await _userRepository.UpdateUser(u);
                return Ok(u);
            }
            return BadRequest();
        }
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var u = await _userRepository.GetUserById(id);
            if(u != null)
            {
                await _userRepository.DeleteUser(u);
                return Ok("Deleted Successfuilly");
            }
            return BadRequest();
        }
    }
}
