using Application.Queries.EmailProjectQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.EmailServiceManager.Pages.MailTask
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<EmailProject> EmailProjects { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            
            ListProjectByUserId listQuery = new ListProjectByUserId();
            EmailProjects = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
