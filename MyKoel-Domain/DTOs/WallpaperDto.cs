using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class WallpaperDto
    {
        public int WallpaperId { get; set; }  
        public string WallpaperName { get; set;}
        public string WallpaperPath { get;set;}
        public bool IsActive { get; set; }
        public string WallpaperSrc { get; set; }

    }
}