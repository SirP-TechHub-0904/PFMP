using Application.Commands.TrainingCommand;
using Application.Commands.EvaluationQuestionCommand;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.TrainingQueries;

namespace RazorWebUI.Areas.ITrainings.Pages.EvaluationPage
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
        public EvaluationQuestion EvaluationQuestion { get; set; }

        [BindProperty]
        public Training Training { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
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
                AddEvaluationQuestionCommand Command = new AddEvaluationQuestionCommand(EvaluationQuestion);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new {id = EvaluationQuestion.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
