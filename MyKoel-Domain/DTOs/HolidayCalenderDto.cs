using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class HolidayCalenderDto
    {
       public int HolidayCalendarId { get; set; }
       public string Holiday{get;set;}
       public DateTime Date {get;set;}
       public string Day {get;set;}
       public string Remarks {get;set;}
       public string Locations {get;set;}
    }

    public class LocationDto
    {
       public string Locations {get;set;}

    }
}