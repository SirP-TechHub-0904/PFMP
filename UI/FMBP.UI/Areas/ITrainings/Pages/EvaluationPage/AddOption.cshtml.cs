using Application.Commands.EvaluationQuestionCommand;
using Application.Commands.EvaluationQuestionOptionCommand;
using Application.Queries.EvaluationQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.EvaluationPage
{
    public class AddOptionModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddOptionModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EvaluationQuestionOption EvaluationQuestionOption { get; set; }
        public EvaluationQuestion EvaluationQuestion { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            GetByIdEvaluationQuestionQuery Command = new GetByIdEvaluationQuestionQuery(id);
            EvaluationQuestion = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddEvaluationQuestionOptionCommand Command = new AddEvaluationQuestionOptionCommand(EvaluationQuestionOption);
                await _mediator.Send(Command);
                TempData["success"] = "Success";

                long tid = EvaluationQuestionOption.EvaluationQuestionId ?? 0;

                GetByIdEvaluationQuestionQuery xCommand = new GetByIdEvaluationQuestionQuery(tid);
                var xEvaluationQuestion = await _mediator.Send(xCommand);

                return RedirectToPage("./Index", new { id = xEvaluationQuestion.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
