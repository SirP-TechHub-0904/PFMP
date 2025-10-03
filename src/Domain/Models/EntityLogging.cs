using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EntityLogging
    { 
        public long Id { get; set; }
        public string? Entity { get; set; }
        public string? SpecificChanges { get; set; }
        public DateTime Date { get; set; }
        public string? LoggedInUserEmail { get; set; }
    }
}
