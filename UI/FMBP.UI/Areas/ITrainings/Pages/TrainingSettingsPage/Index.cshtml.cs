using Application.Queries.TrainingQueries;
using Application.Queries.TrainingSettingQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.TrainingSettingsPage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<TrainingSetting> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            if (Training != null)
            {
                var query = new ListTrainingSettingByTrainingQuery(Training.Id);
                Datas = await _mediator.Send(query);
            }
            else
            {
                return RedirectToPage("/Admin/Index");
            }

            return Page();
        }
    }

}
