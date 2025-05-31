using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Areas.Manager.Pages.Keys
{
    public class IndexModel : PageModel
    {
        private readonly KeyService _service;

        public List<Key> Keys { get; set; } = new();

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public IndexModel(KeyService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Keys = (await _service.GetAllAsync()).ToList();
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
                await _service.SaveAsync(key);
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