using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailListRepository : IRepository<EmailList>
    {
        Task<int> GetEmailsCountByUserId();
        Task<List<EmailList>> ListByUserId();
        Task<List<EmailList>> ListByGroupId(long groupId);

        Task AddMany(List<EmailList> emails);
    }
}
