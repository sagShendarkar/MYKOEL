using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface IWallpaperRepository
    {
          void UpdateWallpaper(Wallpaper wallpaper);
          Task<bool> SaveAllAsync();
          void DeleteWallpaper(Wallpaper wallpaper);
          Task<Wallpaper> GetWallpaperById(int Id);
          void AddNewWallpaper(Wallpaper wallpaper);
           Task<List<BirthdayDto>> GetBirthdayList(DateTime Date);


    }
}