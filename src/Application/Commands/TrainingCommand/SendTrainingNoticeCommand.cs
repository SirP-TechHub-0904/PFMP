using Application.Commands.EmailCommand;
using Application.Commands.SmsCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCommand
{
    public sealed class SendTrainingNoticeCommand : IRequest<bool>
    {
        public SendTrainingNoticeCommand(string? userId, long trainingId, string? title, string? position)
        {
            UserId = userId;
            TrainingId = trainingId;
            Title = title;
            Position = position;
        }

        public string? UserId { get; set; }
        public long TrainingId { get; set; }
        public string? Title { get; set; }
        public string? Position { get; set; }

    }

    public class SendTrainingNoticeCommandHandler : IRequestHandler<SendTrainingNoticeCommand, bool>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainingSettingRepository _trainingsettings;
        private readonly IMediator _mediator;

        public SendTrainingNoticeCommandHandler(ITrainingRepository trainingRepository, IMediator mediator, ITrainingSettingRepository trainingsettings)
        {
            _trainingRepository = trainingRepository;
            _mediator = mediator;
            _trainingsettings = trainingsettings;
        }

        public async Task<bool> Handle(SendTrainingNoticeCommand request, CancellationToken cancellationToken)
        {
           
            try
            {


                var getTraining = await _trainingRepository.GetByIdAsync(request.TrainingId);
                if (getTraining != null)
                {
                    var setting = await _trainingsettings.GetAll(request.TrainingId);

                    string template = "";

                    if (setting.FirstOrDefault() != null)
                    {
                        template = setting.FirstOrDefault().Template;

                    }

                    GetUserByIdQuery getUser = new GetUserByIdQuery(request.UserId);
                    var userdata = await _mediator.Send(getUser);

                    //try
                    //{
                    //    SendSmsCommand smscommand = new SendSmsCommand(userdata.PhoneNumber, "Kindly check your email to complete your registration.");
                    //    var smsresponse = await _mediator.Send(smscommand);

                    //}
                    //catch (Exception c) { }


                    string baseUrl = "https://fmbepnutrition.ng/"; // replace with your application base URL
                    string relativeUrl = "/Identity/Account/Login";
                    Uri baseUri = new Uri(baseUrl);
                    Uri loginUri = new Uri(baseUri, relativeUrl);

                    // Now you can use the loginUri to create the link
                    string linkcomplete = loginUri.AbsoluteUri;

                    string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                    string messagedetails2 = $"Login Email: {userdata.Email} <br>Temporal Password: {userdata.TempPass}<br>" +
                        $"" + datavalue + "or copy the link before and paste in your browser to continue<br><br>" +
                        linkcomplete + "<br>" +

                        $"<h6>Instruction: Kindly login with the temporal password, reset your password and update your profile.</h6>";

                    if (userdata.ResetPassword == false)
                    {
                        messagedetails2 = "";
                    }

                    template = template.Replace("{logininformation}", messagedetails2);
                    template = template.Replace("{fullname}", userdata.FullnameX);
                    template = template.Replace("{surname}", userdata.Surname);

                    string subject = setting.FirstOrDefault().Subject ?? "";
                    //send email
                    MessageDto msn = new MessageDto();
                    msn.Email = userdata.Email;
                    msn.Message = template;
                    msn.Subject = subject;
                    msn.Name = userdata.FullnameX;
                    SendMessageCommand emailcommand = new SendMessageCommand(msn);
                    PostmarkResponse responseemail = await _mediator.Send(emailcommand);

                    if (responseemail.Status == PostmarkStatus.Success)
                    {
                        return true;
                    }

                    return false;

                    

                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return false;
        }
    }
}
