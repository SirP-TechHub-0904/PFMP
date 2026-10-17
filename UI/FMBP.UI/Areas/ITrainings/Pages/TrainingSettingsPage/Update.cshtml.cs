using Application.Commands.TrainingSettingCommand;
using Application.Queries.TrainingSettingQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.TrainingSettingsPage
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
        public TrainingSetting TrainingSetting { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingSettingQuery Command = new GetByIdTrainingSettingQuery(id);
            TrainingSetting = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingSettingCommand Command = new UpdateTrainingSettingCommand(TrainingSetting);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = TrainingSetting.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
