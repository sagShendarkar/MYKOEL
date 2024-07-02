using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{ 
    #nullable disable
    public class MenusDto
    {
      public int MenuId{get;set;}
       public int? MenuGroupId{get;set;}
       public string MenuName {get;set;}
       public int Sequence {get;set;}
      public string Icon{get;set;}
      public bool IsActive{get;set;}
      public string Route{get;set;}
       public List<MenuGroupDto> MenuGroupData{get;set;}

    }
}