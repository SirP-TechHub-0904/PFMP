using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetWebHookUpdateIds
    {
        public string UserId { get; set; }
        public long UserCategoryGroupId { get; set; }
    }
}
