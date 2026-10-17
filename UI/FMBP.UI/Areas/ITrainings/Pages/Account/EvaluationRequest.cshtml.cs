using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FMBP.UI.Areas.ITrainings.Pages.Account
{
    [Authorize]
    public class EvaluationRequestModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public EvaluationRequestModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<TrainingAndDailyActivitiesByUserIdDto> TrainingsWithTodayActivity { get; private set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new ListTrainingsWithTodayActivityByUserQuery(userId);
            TrainingsWithTodayActivity = await _mediator.Send(query);
 
            return Page();
        }
    }
}
