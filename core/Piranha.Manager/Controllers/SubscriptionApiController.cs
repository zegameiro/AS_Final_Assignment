using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/subscription")]
    [Authorize(Policy = Permission.Admin)]
    [ApiController]
    [AutoValidateAntiforgeryToken]
    public class SubscriptionApiController : Controller
    {
        private readonly SubscriptionService _service;

        public SubscriptionApiController(SubscriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var subs = await _service.GetAllAsync();
            return Ok(subs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sub = await _service.GetByIdAsync(id);
            if (sub == null)
                return NotFound();
            return Ok(sub);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Subscription model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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