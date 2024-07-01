using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace industry4_Domain.Entities
{
    public class AuditableEntities
    {
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}