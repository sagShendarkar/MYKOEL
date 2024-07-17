using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class VacancyInquiry
    {
        public int VACANCYINQUIRYID { get; set; }
        public int VACANCYID { get; set; }
        public string FIRSTNAME   { get; set; } 
        public string LASTNAME  { get; set; }
         public string EMAIL   { get; set; } 
        public string PHONENO  { get; set; }
        public int RESUMEPATH   { get; set; } 
        public string COMMENT  { get; set; }

   }
}