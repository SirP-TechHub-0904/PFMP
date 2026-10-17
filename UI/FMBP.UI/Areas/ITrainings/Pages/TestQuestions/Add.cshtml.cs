using Application.Commands.TrainingCommand;
using Application.Commands.TrainingTestCommand;
using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.TestQuestions
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
        public TrainingTest TrainingTest { get; set; }

        [BindProperty]
        public Training Training { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            if(Training == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddTrainingTestCommand Command = new AddTrainingTestCommand(TrainingTest);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new {id = TrainingTest.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
