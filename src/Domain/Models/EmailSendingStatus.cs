using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class EmailSendingStatus
    {
        public long Id { get; set; }

        public string Group { get; set; }

        public long? UserCategoryGroupId { get; set; }
        public UserCategoryGroup UserCategoryGroup {  get; set; }


        public long EmailProjectId { get; set; }
        public EmailProject EmailProject { get; set; }


        public DateTime SubmittedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public SendingStatus SendingStatus { get;set;   }
        public int Retries { get; set; }

        public string? Log {  get; set; }
        public string? MessageId { get; set; }
    }
}
