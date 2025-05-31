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
        [HttpGet]
        [Route("list")]
        public IEnumerable<Event> List()
        {
            return EventConsumer.GetConsumedEvents();
        }
    }
}
