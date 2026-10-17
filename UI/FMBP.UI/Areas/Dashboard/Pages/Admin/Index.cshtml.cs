using Application.Queries.DashboardQueries;
using Application.Queries.IdentityQueries;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FMBP.UI.Areas.Dashboard.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator, AppDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        public AdminDashboardDto Datas { get; private set; }
         public List<UserCategory> UserCategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string state = "";
            var usercommand = new GetUserByIdQuery(userId);
            var userinfo = await _mediator.Send(usercommand);
            if (userinfo != null)
            {
                state = userinfo.State;
            }
            if (User.IsInRole("Admin") || User.IsInRole("mSuperAdmin"))
            {
                state = "all";
            }

            var query = new GetDashboardQuery(state);
            Datas = await _mediator.Send(query);


            UserCategories = await _context.UserCategories
                .Include(x=>x.UserCategoryGroups)
                .ToListAsync();
             

            return Page();
        }

    }
}
