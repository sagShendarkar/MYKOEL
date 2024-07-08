using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Extensions;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                return Ok("User created successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("UserDropdown")]
        public async Task<List<UserDropdown>> MenuList(string? UserName)
        {
            var userData = await _userRepository.GetUserDropdown(UserName);
            return userData;
        }

        [HttpPost("AddUserAccess")]
        public async Task<object> AddUserAccess(List<UserAccessMappingDto> userAccess)
        {
            var userAccessMappings = _mapper.Map<List<UserAccessMapping>>(userAccess);
            foreach (var item in userAccessMappings)
            {
                //var mainmenu= _context.UserMenuMap.Where(m=>  m.UserId==item.UserId).FirstOrDefault();

                if (item.MainMenuGroupId > 0)
                {
                    var mainMenuGroup = new UserAccessMapping
                    {
                        AccessMappingId = 0,
                        MainMenuGroupId = item.MainMenuGroupId,
                        UserId = item.UserId,
                        MenuGroupId = item.MenuGroupId,
                        MenuId = item.MenuId
                    };

                    _userRepository.AddUserMenuAccess(mainMenuGroup); 
                }
                else
                {
                    return new 
                    {
                        Status=400,
                        Message="Invalid Main Menu Name."
                    };
                }
            }
            if( await _userRepository.SaveAllAsync())
            {
              return new 
              {
                  Status=200,
                  Message="Data Saved Successfully"
              };        
            }
            else
            {
              return new 
              {
                  Status=400,
                  Message="Failed To Save Data"
              };  
            }
        }



         [HttpPost("UpdateProfileImage")]
        public async Task<object> UpdateProfile(UserProfileDto userProfileDto)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Id == User.GetUserId()).FirstOrDefaultAsync();
                if(userProfileDto.ProfileSrc != null)
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

                    string userFolderPath = Path.Combine(folderPath, user.TicketNo);

                    if (!Directory.Exists(userFolderPath))
                    {
                        Directory.CreateDirectory(userFolderPath);
                    }

                    string profileFolderPath = Path.Combine(userFolderPath, "Profile Images");
                    if (!Directory.Exists(profileFolderPath))
                    {
                        Directory.CreateDirectory(profileFolderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + ".png";
                    string imagePath = Path.Combine(profileFolderPath, fileName);

                    string base64StringData = userProfileDto.ProfileSrc;
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


                    userProfileDto.ProfileImage = Path.Combine(profileFolderPath, fileName);
                     user = _mapper.Map<AppUser>(userProfileDto);
                    _userRepository.UpdateUser(user);
                 if (await _userRepository.SaveAllAsync())
                {
                    return new 
                    {
                        Status=200,
                        Message="Data Saved Successfully",
                        UserId=user.Id
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