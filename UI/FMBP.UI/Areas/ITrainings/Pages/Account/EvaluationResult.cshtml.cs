using Application.Queries.DialyActivityQueries;
using Application.Queries.DialyUserEvaluationQueries;
using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.UserTestQueries;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FMBP.UI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EvaluationResultModel : PageModel
    {
        private readonly IMediator _mediator;

        public EvaluationResultModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public DialyUserEvaluationResultDto DialyUserEvaluationResultDto { get; set; }
        public Training Training { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public DialyActivity DialyActivity { get; set; }

        public BasicProfileDto BasicProfileDto { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, long tid, long did)
        {
            
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                GetUserByIdQuery userCommand = new GetUserByIdQuery(userId);
                BasicProfileDto = await _mediator.Send(userCommand);
            }
            DialyUserEvaluationResultQuery Command = new DialyUserEvaluationResultQuery(did, userId);
            DialyUserEvaluationResultDto = await _mediator.Send(Command);

            if (DialyUserEvaluationResultDto != null)
            {
                if (DialyUserEvaluationResultDto.DialyUserEvaluation.Count() == 0)
                {
                    TempData["error"] = "No Assessment Found";
                    if (User.IsInRole("Participant"))
                    {
                        return RedirectToPage("./info", new { id = tid });
                    }
                    return RedirectToPage("/Admin/info", new { id = tid });
                }
            }
             

            GetByIdTrainingQuery xCommand = new GetByIdTrainingQuery(tid);
            Training = await _mediator.Send(xCommand);

            var query = new GetByIdDialyActivityQuery(did);
            DialyActivity = await _mediator.Send(query);

            return Page();
        }
    }

}
