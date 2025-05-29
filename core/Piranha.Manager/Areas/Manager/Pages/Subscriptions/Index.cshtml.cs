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

        public IndexModel(SubscriptionService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Subscriptions = (await _service.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}