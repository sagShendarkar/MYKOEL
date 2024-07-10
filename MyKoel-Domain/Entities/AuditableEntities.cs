using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Entities
{
    public class AuditableEntities
    {
        public int CREATEDBY { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public DateTime UPDATEDDATE { get; set; }
    }
}