using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingSetting
    {
        public long Id { get; set; }

        public string? Subject { get; set; }
        public string? Template { get; set; }


        public long TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
