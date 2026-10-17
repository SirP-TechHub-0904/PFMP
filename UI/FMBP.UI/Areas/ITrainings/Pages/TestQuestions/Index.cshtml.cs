using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.TrainingTestQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.TestQuestions
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

        public IEnumerable<TrainingTest> Datas { get; private set; }
        [BindProperty]
        public Training Training { get; set; }
        public async Task OnGetAsync(long id)
        {

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);

            var query = new ListTrainingTestByCategoryQuery(id);
            Datas = await _mediator.Send(query);
        }
    }
}
