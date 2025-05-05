using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserCategoryGroup
    {
        public long Id { get; set; }
        public long UserCategoryId { get; set; }
        public UserCategory UserCategory { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(1);

        public string? ColumnOne { get; set; }
        public string? ColumnTwo { get; set; }
        public string? ColumnThree { get; set; }
        public string? ColumnFour { get; set; }
    }
}
