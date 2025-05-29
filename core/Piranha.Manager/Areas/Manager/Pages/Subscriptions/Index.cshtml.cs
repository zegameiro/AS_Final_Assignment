using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Areas.Manager.Pages.Subscriptions
{
    public class IndexModel : PageModel
    {
        private readonly SubscriptionService _service;

        public List<Subscription> Subscriptions { get; set; } = new();

        [BindProperty]
        public string EventType { get; set; }
        
        [BindProperty]
        public string CallbackUrl { get; set; }

        [BindProperty]
        public string Filter { get; set; }

        [BindProperty]
        public Guid Id { get; set; } // Add this property

        public IndexModel(SubscriptionService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Subscriptions = (await _service.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddOrUpdateAsync()
        {
            if (!string.IsNullOrWhiteSpace(EventType) && !string.IsNullOrWhiteSpace(CallbackUrl))
            {
                var sub = new Subscription
                {
                    Id = Id, // If empty, add; if set, update
                    EventType = EventType,
                    CallbackUrl = CallbackUrl,
                    Filter = Filter
                };
                await _service.SaveAsync(sub);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _service.DeleteAllAsync();
            return RedirectToPage();
        }
    }
}