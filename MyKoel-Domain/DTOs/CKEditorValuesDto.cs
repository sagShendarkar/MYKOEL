using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    public class CKEditorValuesDto
    {
        public int SECTIONID { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool ISIMAGE { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public string FLAG { get; set; }
        public string SEQUENCE { get; set; }
        public bool ISACTIVE { get; set; }
        public string? CATEGORY { get; set; }
        public bool IsHtml { get; set; }

    }
}