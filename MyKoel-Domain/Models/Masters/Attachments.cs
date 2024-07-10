using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Entities;

namespace MyKoel_Domain.Models.Masters
{
    public class Attachments:AuditableEntities
    {
        public int ATTACHMENTID {  get; set;  }
        public int SECTIONID {  get; set;  }
        public string PATH { get; set;}
        public string FILENAME { get; set;  }
        public string FILETYPE { get; set;  }
        public string TITLE { get; set;  }
        public bool ISPOPUP { get; set;  }
        public bool ISREDIRECTED {  get; set; }
        public bool ISACTIVE {  get; set;  }
       public virtual SectionTransaction SectionTransaction { get; set; }

    }
}