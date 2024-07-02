using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class Menus
    {
       public int MenuId{get;set;}
       public int? MenuGroupId{get;set;}
       public string MenuName {get;set;}
       public int Sequence {get;set;}
      public string Icon{get;set;}
      public bool IsActive{get;set;}
      public string Route{get;set;}
      public virtual MenuGroup MenuGroup { get; set; }
      public ICollection<UserAccessMapping> userMenuMaps { get; set; }


    }
}