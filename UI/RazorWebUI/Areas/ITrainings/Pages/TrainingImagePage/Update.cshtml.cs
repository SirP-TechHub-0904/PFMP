using Application.Commands.TrainingCommand;
using Application.Commands.TrainingImageCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.TrainingImageQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.TrainingImagePage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingImage TrainingImage { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingImageQuery Command = new GetByIdTrainingImageQuery(id);
            TrainingImage = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingImageCommand Command = new UpdateTrainingImageCommand(TrainingImage, imagefile);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = TrainingImage.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
