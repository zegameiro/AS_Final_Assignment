using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Areas.Manager.Pages.Subscriptions
{
    public class IndexModel : PageModel
    {
        private readonly IApi _api;

        public List<Subscription> Subscriptions { get; set; } = new();

        [BindProperty]
        public string EventType { get; set; }

        [BindProperty]
        public string EventStatus { get; set; }
        
        [BindProperty]
        public string CallbackUrl { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public string ErrorMessage { get; set; }
        

        [BindProperty]
        public Guid Id { get; set; } // Add this property

        public IndexModel(IApi api)
        {
            _api = api;
        }

        public async Task OnGetAsync()
        {
            Subscriptions = (await _api.Subscriptions.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddOrUpdateAsync()
        {
            if (!string.IsNullOrWhiteSpace(EventType) && !string.IsNullOrWhiteSpace(CallbackUrl))
            {
                var sub = new Subscription
                {
                    Id = Id, // If empty, add; if set, update
                    EventType = EventType,
                    EventStatus = EventStatus,
                    CallbackUrl = CallbackUrl,
                    Tags = Tags
                };

                try
                {
                    await _api.Subscriptions.SaveAsync(sub);
                    return RedirectToPage();
                }
                catch (InvalidOperationException ex)
                {
                    ErrorMessage = ex.Message;
                    await OnGetAsync(); // Refresh the subscriptions list
                    return Page();
                }
            } 
            else
            {
                ModelState.AddModelError(string.Empty, "Event Type and Callback URL are required.");
                await OnGetAsync(); // Refresh the subscriptions list
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _api.Subscriptions.DeleteAsync(id);
                return RedirectToPage();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync(); // Refresh the subscriptions list
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _api.Subscriptions.DeleteAllAsync();
            return RedirectToPage();
        }
    }
}