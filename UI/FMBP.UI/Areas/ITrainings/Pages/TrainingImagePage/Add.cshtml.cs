using Application.Commands.TrainingCommand;
using Application.Commands.TrainingImageCommand;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.TrainingQueries;

namespace FMBP.UI.Areas.ITrainings.Pages.TrainingImagePage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingImage TrainingImage { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }

        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddTrainingImageCommand Command = new AddTrainingImageCommand(TrainingImage, imagefile);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new {id = TrainingImage.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
