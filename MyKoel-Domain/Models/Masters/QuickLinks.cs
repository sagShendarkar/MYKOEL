using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Entities;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class QuickLinks:AuditableEntities
    {
        public int QuickLinkId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string URL { get; set; }
        public int Sequence { get; set; }
        public bool IsActive { get; set; }
    }
}