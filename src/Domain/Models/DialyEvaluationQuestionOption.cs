using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DialyEvaluationQuestionOption
    {
        public long Id { get; set; }
        public string Option { get; set; }
        public int SortOrder { get; set; }

        public long? DialyEvaluationQuestionId { get; set; }
        public DialyEvaluationQuestion? DialyEvaluationQuestion { get; set; }
    }
}
