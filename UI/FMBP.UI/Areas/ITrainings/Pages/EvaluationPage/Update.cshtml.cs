using Application.Commands.TrainingCommand;
using Application.Commands.EvaluationQuestionCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.EvaluationQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.EvaluationPage
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
        public EvaluationQuestion EvaluationQuestion { get; set; }
        [BindProperty]
        public Training Training { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEvaluationQuestionQuery Command = new GetByIdEvaluationQuestionQuery(id);
            EvaluationQuestion = await _mediator.Send(Command);


            GetByIdTrainingQuery xCommand = new GetByIdTrainingQuery(EvaluationQuestion.TrainingId ?? 0);
            Training = await _mediator.Send(xCommand);
            if (Training == null)
            {
                return NotFound();
            }
            return Page();
        }
         



        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateEvaluationQuestionCommand Command = new UpdateEvaluationQuestionCommand(EvaluationQuestion);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = EvaluationQuestion.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
