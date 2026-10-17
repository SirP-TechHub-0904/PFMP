using Application.DTO;
using Application.Queries.EmailResponseStatusQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.EmailServiceManager.Pages.Manager
{
    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public MailInfoDto MailInfoDto { get; set; }


        public async Task<IActionResult> OnGetAsync(long id, string mxs)
        {
            if (id < 0)
            {
                return NotFound();
            }
            ListAllEmailResponseByMessageIdQuery Command = new ListAllEmailResponseByMessageIdQuery(mxs, id);
            MailInfoDto = await _mediator.Send(Command);


            return Page();
        }

    }

}
