using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    public class UserAccessMappingDto
    {
        public int AccessMappingId{get;set;}
        public int MenuId { get; set; }
        public int UserId { get; set; }
       
    }
}