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
        public string Location {get;set;}

        public List<int> LunchId {get;set;}
        public List<int> BreakFastId {get;set;}

    }


    public class CanteenMenuListDto
    {
        public int CanteenMenusId{ get; set; }
        public DateTime Date {get;set;}
        public string Location {get;set;}
        public string Name {get;set;}

    }
    
}