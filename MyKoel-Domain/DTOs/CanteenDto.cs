using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class CanteenDto
    {
        public int CanteenMenusId{ get; set; }
        public DateTime Date {get;set;}
        public int? LunchId {get;set;}
        public int? BreakFastId {get;set;}

    }


    public class CanteenMenuListDto
    {
        public int CanteenMenusId{ get; set; }
        public DateTime Date {get;set;}
        public List<BreakFastDto> BreakfastList {get;set;}
        public List<LunchDto> LunchList {get;set;}

    }
    
}