using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Models.Master
{
  #nullable disable
    public class MenuGroup
    {
       public int MenuGroupId{get;set;}
       public int? MainMenuGroupId{get;set;}
       public string GroupName {get;set;}
       public int Sequence {get;set;}
      public string Icon{get;set;}
      public bool IsActive{get;set;}
      public bool IsChild{get;set;}
      public string Route{get;set;}
      public virtual MainMenuGroup MainMenuGroup { get; set; }
      public ICollection<Menus> Menus { get; set; }

    }
}