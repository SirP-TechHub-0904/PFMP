using Application.Queries.DashboardQueries;
using Application.Queries.IdentityQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RazorWebUI.Areas.Dashboard.Pages.Admin
{
    public class NutritionDashboardModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public NutritionDashboardModel(ILogger<IndexModel> logger, IMediator mediator, AppDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

         public List<UserCategory> UserCategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            

            UserCategories = await _context.UserCategories
                .Include(x => x.UserCategoryGroups)
                .ToListAsync();


            return Page();
        }

    }
}
