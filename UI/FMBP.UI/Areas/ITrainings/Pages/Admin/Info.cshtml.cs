using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.TrainingSettingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingDto Training { get; set; }

        [BindProperty]
        public TrainingCategory TrainingCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(Command);


            GetByIdTrainingCategoryQuery xCommand = new GetByIdTrainingCategoryQuery(id);
            TrainingCategory = await _mediator.Send(xCommand);


            var query = new ListTrainingSettingByTrainingQuery(Training.Id);
            var check = await _mediator.Send(query);

            if (check.Count() == 0)
            { 
                return RedirectToPage("/TrainingSettingsPage/Add", new { id = Training.Id });
            }

            return Page();
        }
    }
}
