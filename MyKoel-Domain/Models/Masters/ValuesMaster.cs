using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Entities;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class ValuesMaster:AuditableEntities
    {
        public int VALUEID { get; set; }
        public string DESCRIPTION   { get; set; } 
        public int TYPE  { get; set; }
        public bool ISACTIVE  { get; set; }

    }
}