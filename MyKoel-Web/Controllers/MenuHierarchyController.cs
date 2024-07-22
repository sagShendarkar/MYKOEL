using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Data;
using MyKoel_Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using MyKoel_Domain.Models.Master;
using AutoMapper;
using iot_Domain.Helpers;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuHierarchyController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMenuHierarchyRepository _menuHierarchy;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public MenuHierarchyController(DataContext context, IMenuHierarchyRepository menuHierarchy, IMapper mapper, IConfiguration config, IUserRepository userRepository)
        {
            _context = context;
            _menuHierarchy = menuHierarchy;
            _mapper = mapper;
            _config = config;
            _userRepository = userRepository;
        }

        [HttpGet("ShowMenuList")]
        public async Task<ActionResult<IEnumerable<MainMenuGroupDto>>> GetMenuList(string? Flag, string? Grade)
        {

            var UserId = User.GetUserId();
            var menu = await _menuHierarchy.GetMenuData(UserId, Flag, Grade);
            return menu;
        }

        [HttpGet("MenuList")]
        public async Task<List<MainMenuGroupDto>> MenuList(string? Name)
        {
            var menu = await _menuHierarchy.GetMenuList(Name);
            return menu;
        }


        [HttpPost("AddMainMenu")]
        public async Task<object> AddMainMenu(AddMainMenuGroupDto mainMenu)
        {
            try
            {

                if (mainMenu.ImageSrc != null)
                {
                    string rootFolderPath = _config.GetValue<string>("RootFolderPath");

                    if (!Directory.Exists(rootFolderPath))
                    {
                        Directory.CreateDirectory(rootFolderPath);
                    }

                    string folderPath = Path.Combine(rootFolderPath, "Quick Links");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + ".Svg";
                    string imagePath = Path.Combine(folderPath, fileName);

                    string base64StringData = mainMenu.ImageSrc;
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

                    mainMenu.ImageIcon = Path.Combine(folderPath, fileName);
                }
                var mainmenus = _mapper.Map<MainMenuGroup>(mainMenu);
                  _menuHierarchy.AddNewMainMenu(mainmenus);
                if (await _menuHierarchy.SaveAllAsync())
                {
                    var users = await _context.Users.Where(s=>s.Grade != "SysAdmin").ToListAsync();
                    foreach (var user in users)
                    {
                        var quicklinkacccess = new UserAccessMapping
                        {
                            AccessMappingId = 0,
                            MainMenuGroupId = mainmenus.MainMenuGroupId,
                            UserId = user.Id,
                            MenuGroupId = null,
                            MenuId = null
                        };
                        _userRepository.AddUserMenuAccess(quicklinkacccess);
                    }
                    await _userRepository.SaveAllAsync();
                    
                    return new
                    {
                        Status = 200,
                        Message = "Data Saved Successfully"
                    };
                }
                else
                {
                    return new
                    {
                        Status = 400,
                        Message = "Failed To Save Data"
                    };

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = $"Failed to update section: {ex.Message}"
                });
            }
        }

        [HttpPost("UpdateMainMenu")]
        public async Task<IActionResult> UpdateMainMenu(AddMainMenuGroupDto mainMenu)
        {
            try
            {
                var existing = await _menuHierarchy.GetMainMenuById(mainMenu.MainMenuGroupId);

                if (existing == null)
                {
                    return NotFound("Section not found");
                }
                if (mainMenu.ImageSrc != null)
                {
                    string rootFolderPath = _config.GetValue<string>("RootFolderPath");

                    if (!Directory.Exists(rootFolderPath))
                    {
                        Directory.CreateDirectory(rootFolderPath);
                    }

                    string folderPath = Path.Combine(rootFolderPath, "Quick Links");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }


                    string fileName = Guid.NewGuid().ToString() + ".Svg";
                    string imagePath = Path.Combine(folderPath, fileName);

                    string base64StringData = mainMenu.ImageSrc;
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

                    mainMenu.ImageIcon = Path.Combine(folderPath, fileName);
                }

                var updatedmainmenu = _mapper.Map(mainMenu, existing);
                _menuHierarchy.UpdateMainMenu(updatedmainmenu);
                if (await _menuHierarchy.SaveAllAsync())
                {
                    return Ok(new
                    {
                        Status = 200,
                        Message = "Main Menu Updated Successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = 400,
                        Message = "Failed To Update Data",

                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = $"Failed To Update Main Menu: {ex.Message}"
                });
            }
        }

        [HttpGet("GetMainMenuDetails")]
        public async Task<AddMainMenuGroupDto> GetMainMenuDetails(int MainMenuId)
        {
            var menu = await _menuHierarchy.GetMainMenuDetails(MainMenuId);
            return menu;
        }

        [HttpGet("ShowMainMenuList")]
        public async Task<List<AddMainMenuGroupDto>> ShowMainMenuList([FromQuery] ParameterParams parameterParams)
        {
            var mainmenuList = await _menuHierarchy.GetMainMenuList(parameterParams);
            Response.AddPaginationHeader(mainmenuList.CurrentPage, mainmenuList.PageSize,
                    mainmenuList.TotalCount, mainmenuList.TotalPages);
            return mainmenuList;
        }

        [HttpPost("DeleteMainMenu/{Id}")]
        public async Task<ActionResult> DeleteCompanyDetails(int Id)
        {

            var data = await _menuHierarchy.GetMainMenuById(Id);
            try
            {
                _menuHierarchy.DeleteMainMenu(data);
                if (await _menuHierarchy.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Deleted Successfully"
                    };
                    return Ok(result);
                }
                return BadRequest("Failed To Delete Data");

            }
            catch (Exception ex)
            {
                var result = new
                {
                    Status = 400,
                    Message = ex.Message
                };
                return BadRequest(result);

            }

        }



       [HttpGet("4thLevelMenuList")]
        public async Task<List<MenuDataListDto>> LevelMenuList(int UserId, int? MenuId, int? Level)
        {
            var menu = await _menuHierarchy.GetMenuLevelsData(UserId,MenuId,Level);
            return menu;
        }


    }
}