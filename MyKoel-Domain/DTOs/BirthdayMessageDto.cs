using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{ 
    #nullable disable
    public class BirthdayMessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Day {get;set;}
        public bool IsActive { get; set; }
    }
}