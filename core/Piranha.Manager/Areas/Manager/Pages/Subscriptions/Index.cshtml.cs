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

        public IndexModel(SubscriptionService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Subscriptions = (await _service.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!string.IsNullOrWhiteSpace(EventType) && !string.IsNullOrWhiteSpace(CallbackUrl))
            {
                Console.WriteLine($"Adding subscription: {EventType} - {CallbackUrl} - {Filter}");
                var sub = new Subscription
                {
                    EventType = EventType,
                    CallbackUrl = CallbackUrl,
                    Filter = Filter
                };
                await _service.SaveAsync(sub);
            }
            Console.WriteLine("Subscription added successfully.");
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