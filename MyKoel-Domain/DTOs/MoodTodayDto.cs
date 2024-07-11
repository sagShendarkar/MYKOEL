using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
#nullable disable
    public class MoodTodayDto
    {
        public int MoodId { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReportedDateTime { get; set; }= DateTime.Now;
        public int UserId { get; set; }

    }
}