using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Models.Master
{
#nullable disable
    public class MoodToday
    {
        public int MoodId { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReportedDateTime { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }  
    }
}