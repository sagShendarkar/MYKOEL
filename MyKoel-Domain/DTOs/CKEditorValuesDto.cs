using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    public class CKEditorValuesDto
    {
        public int SECTIONID {  get; set;  }
        public string DESCRIPTION { get; set;  }
        public bool IsHtml {get;set;}
    }
}