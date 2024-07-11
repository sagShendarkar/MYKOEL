using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    public class LunchDto
    {
         public int LunchId { get; set; }
         public string LunchName { get; set; } 
         public bool IsActive { get; set; }
    }
}