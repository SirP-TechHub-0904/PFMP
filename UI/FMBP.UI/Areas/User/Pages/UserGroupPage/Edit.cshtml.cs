using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMBP.UI.Areas.User.Pages.UserGroupPage
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context) => _context = context;

        [BindProperty] public UserCategory UserCategory { get; set; }

        [BindProperty]
        public long Id { get;set;}
        public async Task<IActionResult> OnGetAsync(long id)
        {
            UserCategory = await _context.UserCategories.FindAsync(id);
            Id = UserCategory.Id;
            return UserCategory == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
             UserCategory.Id = Id;

            _context.Attach(UserCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
