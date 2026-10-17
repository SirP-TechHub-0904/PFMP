using Application.Commands.EvaluationQuestionOptionCommand;
using Application.Queries.EvaluationQuestionOptionQueries;
using Application.Queries.EvaluationQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.EvaluationPage
{
    public class DeleteOptionModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteOptionModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EvaluationQuestionOption EvaluationQuestionOption { get; set; }
        public EvaluationQuestion EvaluationQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEvaluationQuestionOptionQuery Command = new GetByIdEvaluationQuestionOptionQuery(id);
            EvaluationQuestionOption = await _mediator.Send(Command);

            GetByIdEvaluationQuestionQuery xCommand = new GetByIdEvaluationQuestionQuery(EvaluationQuestionOption.EvaluationQuestionId ?? 0);
            EvaluationQuestion = await _mediator.Send(xCommand);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteEvaluationQuestionOptionCommand Command = new DeleteEvaluationQuestionOptionCommand(EvaluationQuestionOption.Id);
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
