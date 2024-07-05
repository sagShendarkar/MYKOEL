using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WallpaperController : ControllerBase
    {
         private readonly IWallpaperRepository _wallpaperRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;       

        public WallpaperController(IWallpaperRepository wallpaperRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _wallpaperRepository = wallpaperRepository;
        }


[HttpPost("AddWallPaper")]
public async Task<ActionResult<WallpaperDto>> AddNewWallpaper(WallpaperDto wallpaperDto)
{
    try
    {
        if (wallpaperDto.WallpaperSrc != null)
        {
            string aratiFolderPath = Path.Combine(Environment.CurrentDirectory, "Arati");
            if (!Directory.Exists(aratiFolderPath))
            {
                Directory.CreateDirectory(aratiFolderPath);
            }

            string wallpaperFolderPath = Path.Combine(aratiFolderPath, "Wallpapers");
            if (!Directory.Exists(wallpaperFolderPath))
            {
                Directory.CreateDirectory(wallpaperFolderPath);
            }

            string fileName = Guid.NewGuid().ToString() + ".png";
            string imagePath = Path.Combine(wallpaperFolderPath, fileName);

            string base64StringData = wallpaperDto.WallpaperSrc;
            string cleandata = base64StringData.Substring(base64StringData.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(cleandata);
            
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
            }
        wallpaperDto.WallpaperName=fileName;
         wallpaperDto.IsActive=true;
            wallpaperDto.WallpaperPath = Path.Combine("Arati", "Wallpapers", fileName);
             }

        var wallpaper = _mapper.Map<Wallpaper>(wallpaperDto);
        _wallpaperRepository.AddNewWallpaper(wallpaper);

        if (await _wallpaperRepository.SaveAllAsync())
        {
            return new WallpaperDto
            {
                WallpaperId = wallpaper.WallpaperId,
                WallpaperName = wallpaper.WallpaperName
            };
        }
        else
        {
            return BadRequest("Failed to add data");
        }
    }
    catch (Exception ex)
    {
        return BadRequest("Failed to add data: " + ex.Message);
    }
}


        
    }
}