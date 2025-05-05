using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.UserGroupPage
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context) => _context = context;

        [BindProperty] public UserCategory UserCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            UserCategory = await _context.UserCategories.FindAsync(id);
            return UserCategory == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserCategory.Id == 0) return NotFound();

            var category = await _context.UserCategories.FindAsync(UserCategory.Id);
            if (category != null)
            {
                _context.UserCategories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
