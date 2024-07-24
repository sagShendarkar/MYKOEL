using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Entities;

namespace MyKoel_Domain.Models.Masters
{
    # nullable disable
    public class BirthdayMessage 
    {
        public int ID { get; set; }
        public string MESSAGE { get; set; }
        public int DAY {get;set;}
        public bool ISACTIVE { get; set; }
    }
}