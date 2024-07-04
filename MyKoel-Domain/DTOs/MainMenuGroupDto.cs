using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class MainMenuGroupDto
    {

      public int MainMenuGroupId{get;set;}
      public string MenuGroupName{get;set;}
      public int Sequence {get;set;}
      public string Icon{get;set;}
      public bool IsActive{get;set;}
      public bool IsChild{get;set;}
      public string Route{get;set;}
      public List<MenuGroupDto> MenuGroupData{get;set;}

    }
    
}