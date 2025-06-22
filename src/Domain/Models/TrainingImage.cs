using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingImage
    {
        public long Id { get; set; }

        public string? Title {  get; set; }
        public string? Url { get; set; }
        public string? Key { get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
