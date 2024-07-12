using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class BreakFastDto
    {
         public int BreakFastId { get; set; }
         public string BreakFastName { get; set; } 
         public bool IsActive { get; set; }
    }
}