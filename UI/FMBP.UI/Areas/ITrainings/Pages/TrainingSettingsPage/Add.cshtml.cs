using Application.Commands.TrainingSettingCommand;
using Application.Queries.TrainingQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.TrainingSettingsPage
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
        public TrainingSetting TrainingSetting { get; set; }
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
                AddTrainingSettingCommand Command = new AddTrainingSettingCommand(TrainingSetting);
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
