using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReportsBackend.Application.Services;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Domain.Helpers;

namespace ReportsBackend.Api.Controllers
{
    [ApiController]
    [Route("admin/screens")]
    //[Authorize]
    public class ScreensController : ControllerBase
    {
        private readonly ScreenService _screenService;

        public ScreensController(ScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenDto>>> GetAll([FromQuery] FindOptions options)
        {
            var screens = await _screenService.GetAllAsync(options);
            return Ok(screens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenDto>> Get(int id)
        {
            var screen = await _screenService.GetByIdAsync(id);
            if (screen == null) return NotFound();
            return Ok(screen);
        }

        [HttpPost]
        public async Task<ActionResult<ScreenDto>> Create([FromBody] ScreenCreateDto dto)
        {
            var created = await _screenService.CreateAsync(dto);
            //return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScreenUpdateDto dto)
        {
            await _screenService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _screenService.DeleteAsync(id);
            return NoContent();
        }
    }
}
