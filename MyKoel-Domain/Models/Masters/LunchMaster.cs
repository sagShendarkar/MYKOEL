using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class LunchMaster
    {
         public int LunchId { get; set; }
         public string LunchName { get; set; } 
         public bool IsActive { get; set; }
    }
}