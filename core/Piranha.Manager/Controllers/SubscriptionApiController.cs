using Microsoft.AspNetCore.Mvc;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/subscription")]
    [ApiController]
    // [AutoValidateAntiforgeryToken]
    public class SubscriptionApiController : Controller
    {
        private readonly IApi _api;

        public SubscriptionApiController(IApi api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var subs = await _api.Subscriptions.GetAllAsync();
            return Ok(subs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sub = await _api.Subscriptions.GetByIdAsync(id);
            if (sub == null)
                return NotFound();
            return Ok(sub);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Subscription model)
        {
            Console.WriteLine("Saving subscription: " + model);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _api.Subscriptions.SaveAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _api.Subscriptions.DeleteAsync(id);
            return Ok();
        }
    }
}