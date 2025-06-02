using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Models;

namespace Piranha.Manager.Areas.Manager.Pages.Keys
{
    public class IndexModel : PageModel
    {
        private readonly IApi _api;

        public List<Key> Keys { get; set; } = new();

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public string ErrorMessage { get; set; }

        public bool ShowModal { get; set; } = false;

        public IndexModel(IApi api)
        {
            _api = api;
        }

        public async Task OnGetAsync()
        {
            Keys = (await _api.Keys.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddOrUpdateAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                var key = new Key
                {
                    Id = Id == Guid.Empty ? Guid.NewGuid() : Id,
                    Name = Name
                };

                try
                {
                    await _api.Keys.SaveAsync(key);
                }
                catch (InvalidOperationException ex)
                {
                    ErrorMessage = ex.Message;
                    await OnGetAsync();
                    return Page();
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _api.Keys.DeleteAsync(id);
                return RedirectToPage();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _api.Keys.DeleteAllAsync();
            return RedirectToPage();
        }
    }
}