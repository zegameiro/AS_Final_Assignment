using Microsoft.AspNetCore.Mvc;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/key")]
    [ApiController]
    public class KeyApiController : Controller
    {
        private readonly KeyService _service;

        public KeyApiController(KeyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var keys = await _service.GetAllAsync();
            return Ok(keys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var key = await _service.GetByIdAsync(id);
            if (key == null)
                return NotFound();
            return Ok(key);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Key model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            var saved = await _service.SaveAsync(model);
            return Ok(saved);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}