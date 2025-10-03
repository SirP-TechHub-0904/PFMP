using Application.Queries.EntityLogsQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminLogsModel : PageModel
    {
        private readonly ILogger<AdminLogsModel> _logger;
        private readonly IMediator _mediator;

        public AdminLogsModel(ILogger<AdminLogsModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<EntityLogging> Logs { get; private set; } = new();

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public string? Search { get; private set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 20, string? search = null)
        {
            var query = new ListEntityLogsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = search
            };

            // ✅ Receive a single result object, not deconstruction
            var result = await _mediator.Send(query);

            Logs = result.Logs;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
            Search = search;
        }
    }
}
