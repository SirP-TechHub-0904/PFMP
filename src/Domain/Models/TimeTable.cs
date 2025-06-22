using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TimeTable
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        public string? Activity {  get; set; }
        public string? Day {  get; set; }

       public string? ResponsiblePerson { get;set;}
       public int SortOrder { get;set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }

    }
}
