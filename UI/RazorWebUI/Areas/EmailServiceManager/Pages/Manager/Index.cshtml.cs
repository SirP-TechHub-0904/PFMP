using Application.Queries.EmailSendingStatusQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.EmailServiceManager.Pages.Manager
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 100;

        //public int PageIndex { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public IEnumerable<EmailSendingStatus> EmailSendingStatus { get; set; }
        public async Task<IActionResult> OnGetAsync(int pagenumber)
        {
            if (pagenumber == 0)
            {
                pagenumber = 1;
            }
            PageNumber = pagenumber;
            

            ListByUserIdEmailSendingStatusQuery listQuery = new ListByUserIdEmailSendingStatusQuery(PageSize, PageNumber);
            EmailSendingStatus = await _mediator.Send(listQuery);



            GetTotalCountEmailSendingStatusQuery countCommand = new GetTotalCountEmailSendingStatusQuery();
            var totalCount = await _mediator.Send(countCommand);
            TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);


            return Page();
        }
    }
}
