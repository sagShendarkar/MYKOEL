using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    public class CanteenMenus
    {
        public int CanteenMenusId{ get; set; }
        public DateTime Date {get;set;}
        public int? LunchId {get;set;}
        public int? BreakFastId {get;set;}
    }
}