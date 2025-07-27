using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReportsBackend.Application.Services;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Domain.Helpers;

namespace ReportsBackend.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    //[Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll([FromQuery] FindOptions options)
        {
            var roles = await _roleService.GetAllAsync(options);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create([FromBody] RoleCreateDto dto)
        {
            var created = await _roleService.CreateAsync(dto);
            //return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateDto dto)
        {
            await _roleService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/permissions")]
        public async Task<IActionResult> GetReportsAndScreens(int id)
        {
            var result = await _roleService.GetReportsAndScreensAsync(id);
            return Ok(result);
        }

        [HttpGet("{userId}/userpermissions")]
        public async Task<IActionResult> GetUserReportsAndScreens(int userId)
        {
            var result = await _roleService.GetUserReportsAndScreensAsync(userId);
            return Ok(result);
        }
    }
}
