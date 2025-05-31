using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Piranha.Events;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/events")]
    [ApiController]
    public class EventsApiController : Controller
    {
        // For demo: static list. Replace with real event queue or storage.
        private static List<Event> _events = new List<Event>();

        [HttpGet]
        [Route("list")]
        public IEnumerable<Event> List()
        {
            return _events;
        }

        // For testing: add event
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] Event ev)
        {
            _events.Add(ev);
            return Ok();
        }
    }
}
