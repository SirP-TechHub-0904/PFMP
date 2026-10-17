using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMBP.UI.Pages.Shared.ViewComponents
{
    public class UserNavigationViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public UserNavigationViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ugroup = await _context.UserCategories.ToListAsync();
            ViewBag.usergroup = ugroup;

            return View();
        }
    }
}
