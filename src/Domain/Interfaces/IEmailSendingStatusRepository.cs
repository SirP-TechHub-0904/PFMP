using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailSendingStatusRepository : IRepository<EmailSendingStatus>
    {
        Task<RegisterGroupEmailsDto> AddEmailsForSending(long projectId, long? groupId, bool sendToAllGroup, string bulkUserInput);
        Task<List<EmailSendingStatus>> ListByUserId();

        Task<IEnumerable<EmailSendingStatus>> GetListByUserIdAsync(int pageNumber, int pageSize);
        Task<IEnumerable<EmailResponseStatus>> GetResponseListByUserIdAsync(int pageNumber, int pageSize);

        Task<int> GetTotalCountByUserIdAsync();
        Task<int> GetResponseTotalCountByUserIdAsync();

        Task SendBatchEmailByEmailIds();

        Task<GetWebHookUpdateIds> GetEmailListIdByMessageId(string  messageId);

        Task<EmailSendingStatus> GetEmailSendingById(long emailId);
    }
}
