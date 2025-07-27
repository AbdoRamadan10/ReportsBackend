using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsBackend.Application.DTOs.Auth;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Application.Services;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Helpers;

namespace ReportsBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] FindOptions options)
        {
            var users = await _userService.GetAllAsync(options);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<ActionResult<UserDto>> AssignRoleToUserAsync(int userId, List<int> roleIds)
        {
            await _userService.AssignRoleToUserAsync(userId, roleIds);
            return Ok("Role has been assigned succesfully");
        }

        [HttpPost("delete-role-from-user")]
        public async Task<ActionResult<UserDto>> DeleteRoleFromUserAsync(int userId, List<int> roleIds)
        {
            await _userService.DeleteRoleFromUserAsync(userId, roleIds);
            return Ok("Role has been deleted succesfully");
        }

    }
}
