using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class BreakFast
    {
         public int BreakFastId { get; set; }
         public string BreakFastName { get; set; } 
         public bool IsActive { get; set; }
       
    }
}