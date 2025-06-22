using Application.Commands.EmailSendingStatusCommands;
using Application.Queries.EmailGroupQueries;
using Application.Queries.EmailProjectQueries;
using Application.Queries.GroupSendingProjectQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.EmailServiceManager.Pages.MailTask
{
    [Authorize]
    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public InfoModel(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [BindProperty]
        public EmailProject EmailProject { get; set; }


        [BindProperty]
        public EmailSendingStatus EmailSendingStatus { get; set; }

        public List<GroupSendingProject> GroupSendingProjects { get; set; }

        [BindProperty]
        public GroupSendingProject GroupModel { get; set; }

        public List<SelectListItem> GroupDropdown { get; set; }

        [BindProperty]
        public string? BulkUserInput { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEmailProjectQuery Command = new GetByIdEmailProjectQuery(id);
            EmailProject = await _mediator.Send(Command);

            //
            ListGroupSendingProjectByProjectIdQuery listMessageGroup = new ListGroupSendingProjectByProjectIdQuery(id);
            GroupSendingProjects = await _mediator.Send(listMessageGroup);
            //
            //ListAllByUserIdEmailGroupQuery listQuery = new ListAllByUserIdEmailGroupQuery(EmailProject.AppUserId);
            //var listGroups = await _mediator.Send(listQuery);


           var UserCategoryList = await _context.UserCategories.ToListAsync();


            GroupDropdown = UserCategoryList.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.Id.ToString(),
                                     Text = a.Title
                                 }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                AddEmailSendingStatusCommand Command = new AddEmailSendingStatusCommand(GroupModel.EmailProjectId, GroupModel.EmailGroupId, GroupModel.AllGroups, BulkUserInput);
                var outcome = await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new { id = GroupModel.EmailProjectId });
            }
            catch (Exception ex)
            {
                GetByIdEmailProjectQuery Command = new GetByIdEmailProjectQuery(GroupModel.EmailProjectId);
                EmailProject = await _mediator.Send(Command);

                //
                ListGroupSendingProjectByProjectIdQuery listMessageGroup = new ListGroupSendingProjectByProjectIdQuery(GroupModel.EmailProjectId);
                GroupSendingProjects = await _mediator.Send(listMessageGroup);
                //
                var UserCategoryList = await _context.UserCategories.ToListAsync();


                GroupDropdown = UserCategoryList.Select(a =>
                                     new SelectListItem
                                     {
                                         Value = a.Id.ToString(),
                                         Text = a.Title
                                     }).ToList();
                return Page();

            }
        }
    }

}
