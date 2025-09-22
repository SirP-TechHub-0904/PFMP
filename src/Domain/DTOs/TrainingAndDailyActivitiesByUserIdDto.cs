using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.DTOs
{
    public class TrainingAndDailyActivitiesByUserIdDto
    {
        public string UserId { get; set; }
        public long TrainingId { get; set; }
         
        public string? TrainingTitle { get; set; }
        public string? TrainingDate { get; set; }
         
        public string? TrainingState { get; set; }
        public string? TrainingLGA { get; set; }


        public ICollection<DailyActivityDto> DailyActivityDto { get; set; }

    }

    public class DailyActivityDto
    {
        public long Id { get; set; }
        public string? Title { get; set; } 
        public DateTime Date { get; set; }
    }
}
