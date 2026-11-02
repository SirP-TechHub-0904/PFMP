using Azure;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostmarkEmailService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repositories
{
    public sealed class EmailSendingStatusRepository : Repository<EmailSendingStatus>, IEmailSendingStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly PostmarkClient _postmarkClient;
        private readonly IConfiguration _configuration;

        public EmailSendingStatusRepository(AppDbContext context, PostmarkClient postmarkClient, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
            _postmarkClient = new PostmarkClient(GetServerTokenFromSettings());
        }
        private string GetServerTokenFromSettings()
        {
            return _configuration?.GetSection("PostmarkSettings")?["ServerToken"] ?? System.Environment.GetEnvironmentVariable("PostmarkSettings__ServerToken") ?? "";
        }
        public async Task<RegisterGroupEmailsDto> AddEmailsForSending(long projectId, long? groupId, bool sendToAllGroup, string bulkUserInput)
        {


            RegisterGroupEmailsDto result = new RegisterGroupEmailsDto();
            string bulkgroupname = "Bulk Entry";
            string groupname = "";

            try
            {
                var getProject = await _context.EmailProjects.FindAsync(projectId);
                


                if (getProject != null)
                {

                    if (!string.IsNullOrWhiteSpace(bulkUserInput))
                    {

                        var entries = bulkUserInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        var newEmails = new List<UserCategoryGroup>();
                        //create group
                        EmailGroup emailGroup = new EmailGroup
                        {
                            Name = bulkgroupname + DateTime.UtcNow.AddHours(1).ToString("ddmmyyyyhhmmss"),
                            
                          
                        };
                        await _context.EmailGroups.AddAsync(emailGroup);
                        await _context.SaveChangesAsync(); // Save the group first to get the ID
                        foreach (var entry in entries)
                        {
                            var parts = entry.Split(':');
                            if (parts.Length == 3)
                            {
                                var name = parts[0].Trim();
                                var email = parts[1].Trim();
                                var phone = parts[2].Trim();

                                var emailEntry = new UserCategoryGroup
                                {
                                    User = new AppUser
                                    {
                                        LastName = name,
                                        Email = email,
                                        PhoneNumber = phone
                                    },
                                };

                                await _context.UserCategoryGroups.AddAsync(emailEntry);
                                newEmails.Add(emailEntry);
                            }
                        }

                        await _context.SaveChangesAsync(); // Save the newly added emails

                        groupname = "Manual Bulk Entry";

                        await AddEmailSendingStatuses(newEmails, groupname, getProject.Id, null);

                        result.Submitted = newEmails.Count;
                        result.Success = true;
                    }

                    if (sendToAllGroup)
                    {
                        //var allEmails = await _context.EmailLists 
                        //    .Distinct()
                        //    .ToListAsync();
                        var allEmails = await _context.UserCategoryGroups
                              .Include(x => x.User)
              .Where(x => x.UserCategoryId == groupId).Distinct()
              .ToListAsync();
                        groupname = "All Groups";

                        await AddEmailSendingStatuses(allEmails, groupname, getProject.Id, null);

                        result.Submitted = allEmails.Count;
                        result.Success = true;
                    }
                    else
                    {
                       // var UserCategoryList = await _context.UserCategories.ToListAsync();

                         

                        var groupemails = await _context.UserCategories.FindAsync(groupId);

                        if (groupemails != null)
                        {

                            //var cccvvgroupEmails = await _context.EmailLists
                            //    .Distinct()
                            //    .ToListAsync();

                            //var groupEmails = await _context.EmailLists
                            //    .Where(x => x.EmailGroupId == groupId)
                            //    .Distinct()
                            //    .ToListAsync();

                            var groupEmails = await _context.UserCategoryGroups
                                .Include(x=>x.User)
                .Where(x => x.UserCategoryId == groupId).Distinct()
                .ToListAsync();



                            groupname = groupemails.Title;

                            await AddEmailSendingStatuses(groupEmails, groupname, getProject.Id, groupId);

                            result.Submitted = groupEmails.Count;
                            result.Success = true;


                        }
                    }

                    //

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<IEnumerable<EmailSendingStatus>> GetListByUserIdAsync(int pageNumber, int pageSize)
        {
            var list = _context.EmailSendingStatuses
                .Include(x => x.UserCategoryGroup)
                .OrderByDescending(e => e.SubmittedDate)
    .Skip(pageSize * (pageNumber - 1)) // Calculate the offset based on the page number
    .Take(pageSize); // Take only the specified number of records per page


            //.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return list;
        }

        public async Task<int> GetTotalCountByUserIdAsync()
        {
            return await _context.EmailSendingStatuses.CountAsync();
        }

        public async Task<List<EmailSendingStatus>> ListByUserId()
        {
            var list = await _context.EmailSendingStatuses.ToListAsync();
            return list;
        }

        public async Task SendBatchEmailByEmailIds()
        {
            var getemails = await _context.EmailSendingStatuses
                .AsNoTracking()
                .Include(x => x.UserCategoryGroup).ThenInclude(x => x.User)
                .Include(x => x.EmailProject)
                .Where(x => x.SendingStatus == EnumStatus.SendingStatus.Pending &&
            x.Retries <= 5).Take(30).ToListAsync();

            foreach (var ms in getemails)
            {
                //get sender email and name
                var server = await _context.Servers.FirstOrDefaultAsync();
                if(server == null)
                {
                    continue;
                }
                //replace the name of the user in the email template.
               
                string emailbody = ms.EmailProject.Template.Replace("$$name$$", ms.UserCategoryGroup.User.FirstName);
                 

                //get the email to be update after sending request.
                var updateSenderMail = await _context.EmailSendingStatuses
                    .Include(x=>x.UserCategoryGroup).ThenInclude(x=>x.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == ms.Id);
                try
                {
                     
                    var message = new PostmarkMessage
                    {
                        From = $"{server.SenderName} <{server.SenderEmail}>",
                        To = ms.UserCategoryGroup.User.Email,
                        Subject = ms.EmailProject.Subject,
                        HtmlBody = emailbody
                    };

                    PostmarkResponse response = await _postmarkClient.SendMessageAsync(message);
                    
                    if (response.Status == 0)
                    {
                        updateSenderMail.SendingStatus = Domain.Models.EnumStatus.SendingStatus.Sent;
                        updateSenderMail.SentDate = DateTime.UtcNow.AddHours(1);
                        updateSenderMail.Log =  response.Message + $"({response.MessageID})";
                        updateSenderMail.MessageId = response.MessageID.ToString();

                        _context.Entry(updateSenderMail).State = EntityState.Modified;
                    }
                    else
                    {
                        updateSenderMail.SendingStatus = Domain.Models.EnumStatus.SendingStatus.Failed;
                    updateSenderMail.Retries += 1;
                        updateSenderMail.Log = response.Message + $"({response.MessageID})";
                        updateSenderMail.MessageId = response.MessageID.ToString();

                        _context.Entry(updateSenderMail).State = EntityState.Modified;
                    }

                }
                catch (Exception ex)
                {
                    updateSenderMail.SendingStatus = Domain.Models.EnumStatus.SendingStatus.Failed;
                    updateSenderMail.Retries += 1;
                    updateSenderMail.Log = ex.ToString();

                    _context.Entry(updateSenderMail).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();

        }

        private async Task AddEmailSendingStatuses(IEnumerable<UserCategoryGroup> emails, string groupname, long projectId, long? groupId)
        {
            var distinctEmails = emails.Select(x => x.User.Email).Distinct();
            int count = 0;
            foreach (var email in distinctEmails)
            {
                EmailSendingStatus sendmail = new EmailSendingStatus
                {
                    SendingStatus = Domain.Models.EnumStatus.SendingStatus.Pending,
                    Retries = 0,
                    UserCategoryGroupId = emails.FirstOrDefault(x => x.User.Email == email).Id,
                    Group = groupname,
                    EmailProjectId = projectId,
                    SubmittedDate = DateTime.UtcNow.AddHours(1),
                };
                count++;
                await _context.EmailSendingStatuses.AddAsync(sendmail);
            }
            GroupSendingProject groupSendingProject = new GroupSendingProject();
            groupSendingProject.Submitted = count;
            groupSendingProject.EmailProjectId = projectId;
            if (groupId == null)
            {
                groupSendingProject.AllGroups = true;
            }
            else
            {
                groupSendingProject.EmailGroupId = groupId;
            }
            groupSendingProject.Date = DateTime.UtcNow.AddHours(1);
            await _context.GroupSendingProjects.AddAsync(groupSendingProject);
            await _context.SaveChangesAsync();
        }

        public async Task<GetWebHookUpdateIds> GetEmailListIdByMessageId(string messageId)
        {
            GetWebHookUpdateIds result = new GetWebHookUpdateIds();
            var data = await _context.EmailSendingStatuses.FirstOrDefaultAsync(x=>x.MessageId == messageId);
            result.UserCategoryGroupId = data.UserCategoryGroupId ?? 0; 
             return result;
        }

        public async Task<EmailSendingStatus> GetEmailSendingById(long emailId)
        {
           var data = await _context.EmailSendingStatuses
                .Include(x=>x.UserCategoryGroup)
                .Include(x=>x.EmailProject)
                .FirstOrDefaultAsync(x=>x.Id == emailId);

            return data;
        }

        public async Task<IEnumerable<EmailResponseStatus>> GetResponseListByUserIdAsync(int pageNumber, int pageSize)
        {
            var list = _context.EmailResponseStatuses
                 .Include(x => x.UserCategoryGroup)
                 .OrderByDescending(e => e.SentDate)
     .Skip(pageSize * (pageNumber - 1)) // Calculate the offset based on the page number
     .Take(pageSize); // Take only the specified number of records per page


            //.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return list;
        }

        public async Task<int> GetResponseTotalCountByUserIdAsync()
        {
            return await _context.EmailResponseStatuses.CountAsync();
        }
    }
}
