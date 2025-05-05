using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.User.Pages.UserGroupPage
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context) => _context = context;

        public IList<UserCategory> UserCategoryList { get; set; } = new List<UserCategory>();

        public async Task OnGetAsync() =>
            UserCategoryList = await _context.UserCategories.ToListAsync();
    }
}
