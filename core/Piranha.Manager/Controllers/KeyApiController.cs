using Microsoft.AspNetCore.Mvc;
using Piranha.Models;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/key")]
    [ApiController]
    public class KeyApiController : Controller
    {
        private readonly IApi _api;

        public KeyApiController(IApi api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var keys = await _api.Keys.GetAllAsync();
            return Ok(keys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var key = await _api.Keys.GetByIdAsync(id);
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

            try
            {
                var saved = await _api.Keys.SaveAsync(model);
                return Ok(saved);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _api.Keys.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}