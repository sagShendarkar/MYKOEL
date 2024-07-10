using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class AddSectionTrnDto
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
        public AttachmentDto Attachment { get; set; }
    }

    public class AttachmentDto{
        public int ATTACHMENTID {  get; set;  }
        public int SECTIONID {  get; set;  }
        public string PATH { get; set;}
        public string FILENAME { get; set;  }
        public string FILETYPE { get; set;  }
        public string TITLE { get; set;  }
        public bool ISPOPUP { get; set;  }
        public bool ISREDIRECTED {  get; set; }
        public string IMAGESRC{get;set;}
        public bool ISACTIVE {  get; set;  }
    }
}