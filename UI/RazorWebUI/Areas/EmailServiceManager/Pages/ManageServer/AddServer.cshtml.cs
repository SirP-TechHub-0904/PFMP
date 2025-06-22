using Application.Commands.ServerCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.EmailServiceManager.Pages.ManageServer
{

    [Authorize]
    public class AddServerModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddServerModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Server Server { get; set; }


        [BindProperty]
        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
           
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddServerCommand Command = new AddServerCommand(Server);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./ServerByUser");
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }

}
