using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Models.Masters
{
    #nullable disable
    public class Wallpaper
    {
        public int WallpaperId { get; set; }  
        public string WallpaperName { get; set;}
        public string WallpaperPath { get;set;}
        public bool IsActive { get; set; }
        public int UserId{get;set;}

    }
}