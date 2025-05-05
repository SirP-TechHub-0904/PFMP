using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.UserGroupPage
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context) => _context = context;

        [BindProperty]
        public UserCategory UserCategory { get; set; }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.UserCategories.Add(UserCategory);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
