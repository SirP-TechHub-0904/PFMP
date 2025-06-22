using Amazon.Runtime.Internal;
using Application.Commands.EmailCommand;
using Application.Commands.IdentityCommand;
using Application.Commands.SmsCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.IdentityQueries;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostmarkEmailService;
using System.Text.Encodings.Web;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class RegistrationStatusModel : PageModel
    {

        private readonly ILogger<RegistrationStatusModel> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public RegistrationStatusModel(ILogger<RegistrationStatusModel> logger, IMediator mediator, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
        }
        [BindProperty]
        public BasicProfileDto UserDatas { get; private set; }
        [BindProperty]
        public bool SendEmail { get; set; }
        [BindProperty]
        public bool SendSMS { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Rx { get; set; }
        [BindProperty]
        public long TrId { get; set; }
        public async Task<ActionResult> OnGetAsync(string id, string? rx, long txid = 0)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserByIdQuery(id);
            UserDatas = await _mediator.Send(query);
            Rx = rx;
            TrId = txid;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            //var query = new ResendSmsEmailCommand(UserId, SendSMS, SendEmail);
            //var result = await _mediator.Send(query);
            var UserDatas = await _userManager.FindByIdAsync(UserId);
            if (SendEmail)
            {
                SendTrainingNoticeCommand sendmailcommand = new SendTrainingNoticeCommand(UserId,
                                TrId, "", "");
                var result = await _mediator.Send(sendmailcommand);



                if (result == true)
                {
                    UserDatas.EmailSent = true;
                    await _userManager.UpdateAsync(UserDatas);
                    TempData["success"] = "Successful";
                }
                else
                {
                    TempData["error"] = "Failed";

                }
            }
            if (SendSMS)
            {
               
                SendSmsCommand smscommand = new SendSmsCommand(UserDatas.PhoneNumber, "Kindly check your email to complete your registration.");
                var result = await _mediator.Send(smscommand);
                //var result = await _mediator.Send(sendmailcommand);



                if (result.Contains("Ok"))
                {
                    UserDatas.SmsSent = true;
                    await _userManager.UpdateAsync(UserDatas);
                    TempData["success"] = "Successful";
                }
                else
                {
                    TempData["error"] = "Failed";

                }
            }
            return RedirectToPage("./RegistrationStatus", new { id = UserId, rx = Rx, txid = TrId });
        }

    }

}