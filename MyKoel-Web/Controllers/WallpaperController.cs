using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Extensions;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;
using System.IO;
using MyKoel_Domain.Migrations;
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
        public async Task<object> AddOrUpdateWallpaper(WallpaperDto wallpaperDto)
        {
            try
            {
                string token = await _context.Users
                        .Where(u => u.Id == User.GetUserId())
                        .Select(s => s.TicketNo)
                        .FirstOrDefaultAsync();
                    if(wallpaperDto.WallpaperSrc != null)
                    {
                    string rootFolderPath = @"C:\MyKoelImages";

                    if (!Directory.Exists(rootFolderPath))
                    {
                        Directory.CreateDirectory(rootFolderPath);
                    }

                    string folderPath = Path.Combine(rootFolderPath, "Employee Configs");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string userFolderPath = Path.Combine(folderPath, token);

                    if (!Directory.Exists(userFolderPath))
                    {
                        Directory.CreateDirectory(userFolderPath);
                    }

                    string wallpaperFolderPath = Path.Combine(userFolderPath, "Wallpapers");
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
                    wallpaperDto.WallpaperName = fileName;
                    wallpaperDto.IsActive = true;
                    wallpaperDto.WallpaperPath = Path.Combine(wallpaperFolderPath, fileName);
                    wallpaperDto.UserId=wallpaperDto.UserId;
                Wallpaper wallpaper;
                if (wallpaperDto.WallpaperId == 0)
                {
                    wallpaper = _mapper.Map<Wallpaper>(wallpaperDto);
                    _wallpaperRepository.AddNewWallpaper(wallpaper);
                }
                else
                {
                    wallpaper = await _wallpaperRepository.GetWallpaperById(wallpaperDto.WallpaperId);
                    if (wallpaper == null)
                    {
                        return NotFound("Wallpaper not found");
                    }
                    wallpaperDto.WallpaperName = fileName;
                    wallpaperDto.WallpaperPath = Path.Combine(wallpaperFolderPath, fileName);
                    wallpaper =_mapper.Map(wallpaperDto, wallpaper);
                    _wallpaperRepository.UpdateWallpaper(wallpaper);
                }

                if (await _wallpaperRepository.SaveAllAsync())
                {
                    return new 
                    {
                        Status=200,
                        Message="Data Saved Successfully",
                        WallpaperId = wallpaper.WallpaperId,
                        WallpaperName = wallpaper.WallpaperName
                    };
                }
                else
                {
                    return new {
                        Status=400,
                        Message="Failed to save changes"
                    };
                }
             }
               else
                {
                  return new {
                        Status=400,
                        Message="Please select Image"
                    };
                }

               
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add/update data: " + ex.Message);
            }
        }


    }
}