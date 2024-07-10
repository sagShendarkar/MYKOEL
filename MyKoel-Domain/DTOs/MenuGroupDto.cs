using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class MenuGroupDto
    {
       public int MenuGroupId{get;set;}
       public int? MainMenuGroupId{get;set;}
       public string GroupName {get;set;}
       public int Sequence {get;set;}
      public string Icon{get;set;}
      public bool IsActive{get;set;}
      public bool IsChild{get;set;}
      public string Route{get;set;}
      public List<MenusDto> MenusData{get;set;}

    }
}