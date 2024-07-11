using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Entities;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class SectionTransaction:AuditableEntities
    {
        public int SECTIONID {  get; set;  }
        public string TITLE { get; set;  }
        public string DESCRIPTION { get; set;  }
        public bool ISIMAGE { get; set;  }
        public DateTime STARTDATE{ get; set; }
        public DateTime ENDDATE{ get; set; }
        public string FLAG {  get; set; }
        public string SEQUENCE {  get; set; }
        public bool ISACTIVE {  get; set;  }
        public string? CATEGORY {get;set;}
        public ICollection<Attachments> Attachments { get; set; }

    }
}