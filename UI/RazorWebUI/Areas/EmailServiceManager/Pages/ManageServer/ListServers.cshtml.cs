using Application.Queries.ServerQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.EmailServiceManager.Pages.ManageServer
{
    [Authorize]
    public class ListServersModel : PageModel
    {
        private readonly IMediator _mediator;

        public ListServersModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<Server> Servers { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ListAllServerQuery listQuery = new ListAllServerQuery();
            Servers = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
