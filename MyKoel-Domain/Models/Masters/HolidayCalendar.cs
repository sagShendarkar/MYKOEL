using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class HolidayCalendar
    {
       public int HOLIDAYCALENDERID   { get; set; }
       public string HOLIDAY {get;set;}
       public DateTime DATE  {get;set;}
       public string DAY  {get;set;}
       public string REMARKS  {get;set;}
       public string LOCATION  {get;set;}
       public string YEAR  {get;set;}
       public bool ISACTIVE  {get;set;}
       public string BATCHID  {get;set;}
    }
}