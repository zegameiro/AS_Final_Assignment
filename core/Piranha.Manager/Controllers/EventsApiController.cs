using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piranha.Events;
using Piranha.Manager.Services;
using Piranha.Models;

namespace Piranha.Manager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/events")]
    [ApiController]
    public class EventsApiController : Controller
    {
        private readonly KeyService _keyService;
        private readonly IApi _api;

        public EventsApiController(KeyService keyService, IApi api)
        {
            _keyService = keyService;
            _api = api;
        }

        // Helper method for API key validation
        private async Task<bool> IsAuthorizedAsync([FromHeader(Name = "X-API-Key")] string headerApiKey, [FromQuery(Name = "apiKey")] string queryApiKey)
        {
            var apiKey = headerApiKey ?? queryApiKey;
            if (string.IsNullOrEmpty(apiKey))
                return false;

            var key = await _keyService.GetByIdAsync(Guid.Parse(apiKey));
            return key != null;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Event> List() 
        {
            return EventConsumer.GetConsumedEvents();
        }
 
        [HttpPost("publish/")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PublishMedia(
            [FromForm] Piranha.Manager.Models.MediaUploadModel model,
            [FromForm] string Tags,
            [FromHeader(Name = "X-API-Key")] string headerApiKey,
            [FromQuery(Name = "apiKey")] string queryApiKey)
        {
            if (!await IsAuthorizedAsync(headerApiKey, queryApiKey))
                return Unauthorized();

            // Save the media using your MediaService or _api
            var uploaded = 0;
            foreach (var upload in model.Uploads)
            {
                if (upload.Length > 0 && !string.IsNullOrWhiteSpace(upload.ContentType))
                {
                    using (var stream = upload.OpenReadStream())
                    {
                        await _api.Media.SaveAsync(new Piranha.Models.StreamMediaContent
                        {
                            Id = model.Uploads.Count() == 1 ? model.Id : null,
                            FolderId = model.ParentId,
                            Filename = System.IO.Path.GetFileName(upload.FileName),
                            Data = stream,
                            Tags = Tags
                        });
                        uploaded++;
                    }
                }
            }

            return Ok(new { success = uploaded > 0, message = $"{uploaded} file(s) published" });
        }
    }
}
