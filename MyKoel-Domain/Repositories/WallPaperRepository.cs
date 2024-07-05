using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Repositories
{
    public class WallPaperRepository:IWallpaperRepository,IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        public WallPaperRepository(DataContext context)
        {
            _context = context;
        }
    
          public async Task<Wallpaper> GetWallpaperById(int Id) 
        {
             var Wallpaper = await _context.wallpaper
            .SingleOrDefaultAsync(x=>x.WallpaperId==Id);
            return Wallpaper;
        }
        

         public void UpdateWallpaper(Wallpaper WallPaper)
        {
            _context.Entry(WallPaper).State=EntityState.Modified;

        }

        public void DeleteWallpaper(Wallpaper WallPaper)
        {
             WallPaper.IsActive=false;
             _context.Entry(WallPaper).State=EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
       public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(isDisposed) return;
            if (disposing)
            {
                // free managed resources
            }
            isDisposed = true;
            // free native resources here if there are any
        }

        public void AddNewWallpaper(Wallpaper Wallpaper)
        {
            _context.Entry(Wallpaper).State=EntityState.Added;
        }

    }
}