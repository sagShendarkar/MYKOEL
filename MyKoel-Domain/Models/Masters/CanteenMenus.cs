using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class CanteenMenus
    {
        public int CANTEENMENUSID { get; set; }
        public DateTime DATE  {get;set;}
        public int? LUNCHID  {get;set;}
        public int? BREAKFASTID  {get;set;}
        public string? Location {get;set;}

    }
}