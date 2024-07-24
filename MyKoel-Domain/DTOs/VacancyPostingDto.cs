using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class VacancyPostingDto
    {
        public int VACANCYID { get; set; }
        public string GRADE   { get; set; } 
        public string JOBTITLE  { get; set; }
         public string VACANCYCOUNT  { get; set; }
         public string DEPARTMENT   { get; set; } 
        public string LOCATION  { get; set; }
        public int JOBTYPE   { get; set; } 
        public string SALARYRANGE  { get; set; }
        public string JOBDESC   { get; set; } 
        public string REQUIRMENTS  { get; set; }
         public DateTime POSTEDDATE   { get; set; } 
        public DateTime CLOSINGDATE  { get; set; }
        public string CONTACTINFO   { get; set; } 
        public int STATUS  { get; set; }
        public bool ISACTIVE {get;set;}
        public string FILESTRING { get; set; }
        public string FILETYPE { get; set; }
        public string FILEPATH { get; set; }

    }

    public class JobDescriptionDto
    {
        public int VACANCYID { get; set; }
        public string GRADE   { get; set; } 
        public string JOBTITLE  { get; set; }
         public string VACANCYCOUNT  { get; set; }
         public string DEPARTMENT   { get; set; } 
        public string JOBDESC   { get; set; } 
    }
}