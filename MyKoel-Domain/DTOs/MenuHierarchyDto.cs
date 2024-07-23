using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class MenuHierarchyDto
    {        
      
    }
    

    public class ThirdLevelMenu
    {
        public int MenusId { get; set; }
        public string MenuName { get; set; }
        public int? ParentId { get; set; } 
        public int? Level { get; set; }
        public int Sequence { get; set; }
        public string Icon { get; set; }
        public bool IsChild { get; set; }
        public string Route { get; set; }
        public string ImageIcon { get; set; }
        public string Flag { get; set; }
        public bool? IsImage { get; set; }
        public bool? IsRoute { get; set; }
        public bool? IsPopup { get; set; }
        public bool? IsIcon { get; set; }
        public bool IsActive { get; set; }
        public List<FourthLevelMenu> FourthLevelMenuList {get; set;}

    }
    public class FourthLevelMenu
    {
        public int MenusId { get; set; }
        public string MenuName { get; set; }
         public int? ParentId { get; set; } 
        public int? Level { get; set; }
        public int Sequence { get; set; }
        public string Icon { get; set; }
        public bool IsChild { get; set; }
        public string Route { get; set; }
        public string ImageIcon { get; set; }
        public string Flag { get; set; }
        public bool? IsImage { get; set; }
        public bool? IsRoute { get; set; }
        public bool? IsPopup { get; set; }
        public bool? IsIcon { get; set; }
        public bool IsActive { get; set; }
    }
    
}