using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Models.Master
{
    #nullable disable
    public class UserAccessMapping
    {  
       public int AccessMappingId{get;set;}
        public int? MainMenuGroupId { get; set; }
        public int? MenuGroupId { get; set; } 
        public int? MenuId { get; set; }
        public int UserId { get; set; }
        public int? MenusId { get; set; }
        public AppUser User { get; set; }
        public virtual Menus Menu { get; set; }


    }
}