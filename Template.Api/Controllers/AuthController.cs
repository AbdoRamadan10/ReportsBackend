using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsBackend.Application.DTOs.Auth;
using ReportsBackend.Application.Interfaces;
using ReportsBackend.Domain.Exceptions;

namespace ReportsBackend.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);

            if (user == null)
                throw new UnauthorizedException("Invalid Credentials");

            var userRoles = await _userService.GetUserRoles(user.Id);

            var token = _tokenService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Expiration = DateTime.Now.AddMinutes(60),
                //Roles = userRoles,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                }

            });
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok(new { message = "Token is valid" });
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register(
    [FromBody] RegisterRequest request)
        {
            // Manual validation (if not using automatic ModelState validation)


            // Check if email already exists
            if (await _userService.EmailExists(request.Email))
            {
                return Conflict("Email already registered");
            }

            // Check if username exists
            if (await _userService.UsernameExists(request.Username))
            {
                return Conflict("Username already taken");
            }

            var user = await _userService.Register(request);
            var token = _tokenService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Expiration = DateTime.Now.AddMinutes(60)
            });
        }


    }
}
