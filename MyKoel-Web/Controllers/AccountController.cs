using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using Newtonsoft.Json;
using Industry4.TPAIntegrations;
using MyKoel_Domain.Extensions;
using MyKoel_Domain.Models.Master;
using AutoMapper;
using MyKoel_Domain.Migrations;

namespace MyKoel_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly AssetDetails _assetDetails;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context,
      IConfiguration configuration, ITokenService tokenService, AssetDetails assetDetails, IMapper mapper, IUserRepository userRepository, IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _assetDetails = assetDetails;
            _mapper = mapper;
            _imageService = imageService;

        }

        //   [HttpPost("login")]
        //   public async Task<ActionResult<string>> Login(LoginDto loginDto)
        //   {
        //       var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        //       if (user == null)
        //       {
        //           var Data = new[]
        //           {
        //               new { status = 400, ErrorMessage = "Invalid Username!" }
        //              };
        //           return JsonConvert.SerializeObject(Data);
        //       }

        //       var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        //       if (!result.Succeeded) {
        //           var Data = new[]
        //           {
        //               new { status = 400, ErrorMessage = "Invalid Login Details!" }
        //              };
        //           return JsonConvert.SerializeObject(Data);
        //       }  

        //       var generatedToken = await _tokenService.CreateToken(user, 7 * 24 * 60);


        //       var usersData = new[]
        //       {
        //           new
        //           {
        //               status = 200,
        //               Username = user.UserName,
        //               UserId = user.Id,
        //               Token = generatedToken
        //           }
        //       };

        //       return JsonConvert.SerializeObject(usersData);

        //   }


        [HttpPost("login")]
        public async Task<object> AdAuthLogin(LoginDto loginDto, string? IsADAuth)
        {
            try
            {
                if (IsADAuth == "0")
                {
                    var usersdata = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

                    var token = await _userManager.GeneratePasswordResetTokenAsync(usersdata);
                    var changePasswordresult = await _userManager.ResetPasswordAsync(usersdata, token, loginDto.Password);

                    var result = await _signInManager.CheckPasswordSignInAsync(usersdata, loginDto.Password, false);
                    if (!result.Succeeded)
                    {
                        var Data = new[]
                        {
                new
                {
                    status = 400,
                    ErrorMessage = "Invalid Login Details!"
                }
            };
                        return JsonConvert.SerializeObject(Data);
                    }
                    var generatedToken = await _tokenService.CreateToken(usersdata, 7 * 24 * 60);

                    var responseData = new
                    {
                        status = 200,
                        Username = usersdata.Email.ToString().Split(".")[0],
                        Token = generatedToken,
                        UserId = usersdata.Id,
                        Email = usersdata.Email,
                        Department = usersdata.Department,
                        ProfileImage = !string.IsNullOrEmpty(usersdata.ProfileImage) ? _imageService.ConvertLocalImageToBase64(usersdata.ProfileImage) : null,
                        WallPaperDetails = await (from w in _context.wallpaper
                                                  where w.UserId == usersdata.Id
                                                  select new WallpaperDto
                                                  {
                                                      WallpaperId = w.WallpaperId,
                                                      WallpaperName = w.WallpaperName,
                                                      WallpaperPath = !string.IsNullOrEmpty(w.WallpaperPath) ? _imageService.ConvertLocalImageToBase64(w.WallpaperPath) : null,
                                                      UserId = w.UserId,

                                                  }).FirstOrDefaultAsync(),
                        IsMoodFilled = await (from w in _context.MoodToday
                                              where w.UserId == usersdata.Id && w.ReportedDateTime.Date == DateTime.Now.Date
                                              orderby w.MoodId descending
                                              select new MoodTodayDto
                                              {
                                                  ReportedDateTime = w.ReportedDateTime,
                                                  Comment = w.Comment,
                                                  MoodId = w.MoodId,
                                                  Rating = w.Rating,
                                              }).FirstOrDefaultAsync(),
                        Grade = usersdata.Grade

                    };
                    return Ok(JsonConvert.SerializeObject(responseData));

                }

                bool AdAuthValid = await _assetDetails.CheckADCredentials(loginDto.Username, loginDto.Password);

                if (AdAuthValid)
                {
                    var userdata = await _userManager.Users.FirstOrDefaultAsync(x => x.TicketNo == loginDto.Username);
                    if (userdata == null)
                    {
                        EmployeeDetails employeeDetails = await _assetDetails.GetEmployeeDetailsAsync(loginDto.Username);
                        var usermodel = new AppUser
                        {
                            UserName = employeeDetails.Table[0].EmpName.Replace(" ", "."),
                            Email = employeeDetails.Table[0].EmailID,
                            TicketNo = loginDto.Username,
                            SBUNo = employeeDetails.Table[0].SBUNo,
                            CostCode = employeeDetails.Table[0].CostCode,
                            Department = employeeDetails.Table[0].Department,
                            Grade = employeeDetails.Table[0].Grade,
                            EMPID = employeeDetails.Table[0].EMPID,
                            Location = employeeDetails.Table[0].Location,
                            MANAGERID = employeeDetails.Table[0].MANAGERID,
                            AppName = employeeDetails.Table[0].AppName,
                            ManagerEmailID = employeeDetails.Table[0].ManagerEmailID,
                            ManagerTicketNo = employeeDetails.Table[0].ManagerTicketNo,
                            DOB = employeeDetails.Table[0].DOB

                        };
                        var resultdata = await _userManager.CreateAsync(usermodel, loginDto.Password);
                        var result = await _signInManager.CheckPasswordSignInAsync(usermodel, loginDto.Password, false);
                        if (!result.Succeeded)
                        {
                            var Data = new[]
                            {
                     new
                     {
                       status = 400,
                       ErrorMessage = "Invalid Login Details!"
                     }
                };
                            return JsonConvert.SerializeObject(Data);
                        }
                        // added default access for profile,links and footer menus
                        var menulist = await _context.MainMenuGroups.Where(s => s.Flag.ToLower().Contains(("Top MenuBar").ToLower()) || s.Flag.ToLower().Contains(("Quick Links").ToLower())
                        || s.Flag.ToLower().Contains(("Footer Menus").ToLower()) || s.Flag.ToLower().Contains(("Wallpaper Menus").ToLower())).ToListAsync();
                        var userAccess = new List<UserAccessMappingDto>();
                        foreach (var item in menulist)
                        {


                            if (item.MainMenuGroupId > 0)
                            {

                                var mainMenuGroup = new UserAccessMappingDto
                                {
                                    AccessMappingId = 0,
                                    MainMenuGroupId = item.MainMenuGroupId,
                                    UserId = usermodel.Id,
                                    MenuGroupId = null,
                                    MenuId = null
                                };
                                userAccess.Add(mainMenuGroup);
                                var menugroup = _context.MenuGroups.Where(s => s.MainMenuGroupId == item.MainMenuGroupId).ToList();
                                if (menugroup.Count == 0)
                                {
                                    var MenuGroup = new UserAccessMappingDto
                                    {
                                        AccessMappingId = 0,
                                        MainMenuGroupId = item.MainMenuGroupId,
                                        UserId = usermodel.Id,
                                        MenuGroupId = null,
                                        MenuId = null
                                    };
                                    userAccess.Add(MenuGroup);
                                }

                                foreach (var item2 in menugroup)
                                {

                                    var menus = _context.Menus.Where(s => s.MenuGroupId == item2.MenuGroupId).ToList();

                                    foreach (var item3 in menus)
                                    {
                                        var MenusGroup = new UserAccessMappingDto
                                        {
                                            AccessMappingId = 0,
                                            MainMenuGroupId = item.MainMenuGroupId,
                                            UserId = usermodel.Id,
                                            MenuGroupId = item2.MenuGroupId,
                                            MenuId = item3.MenuId
                                        };
                                        userAccess.Add(MenusGroup);
                                    }
                                }

                            }

                        }
                        var userAccessMappings = _mapper.Map<List<UserAccessMapping>>(userAccess);
                        _context.UserMenuMap.AddRange(userAccessMappings);
                        await _context.SaveChangesAsync();
                        //
                        var generatedToken = await _tokenService.CreateToken(usermodel, 7 * 24 * 60);
                        var responseData = new
                        {
                            Status = 200,
                            Username = usermodel.Email.ToString().Split(".")[0],
                            Token = generatedToken,
                            UserId = usermodel.Id,
                            Email = usermodel.Email,
                            Department = usermodel.Department,
                            Grade= usermodel.Grade,
                            ProfileImage = !string.IsNullOrEmpty(usermodel.ProfileImage) ? _imageService.ConvertLocalImageToBase64(usermodel.ProfileImage) : null,
                            WallPaperDetails = await (from w in _context.wallpaper
                                                      where w.UserId == usermodel.Id
                                                      select new WallpaperDto
                                                      {
                                                          WallpaperId = w.WallpaperId,
                                                          WallpaperName = w.WallpaperName,
                                                          WallpaperPath = !string.IsNullOrEmpty(w.WallpaperPath) ? _imageService.ConvertLocalImageToBase64(w.WallpaperPath) : null,
                                                          UserId = w.UserId
                                                      }).FirstOrDefaultAsync(),
                        };


                        return Ok(JsonConvert.SerializeObject(responseData));
                    }

                    else
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(userdata);
                        var changePasswordresult = await _userManager.ResetPasswordAsync(userdata, token, loginDto.Password);

                        var result = await _signInManager.CheckPasswordSignInAsync(userdata, loginDto.Password, false);
                        if (!result.Succeeded)
                        {
                            var Data = new[]
                            {
                new
                {
                    status = 400,
                    ErrorMessage = "Invalid Login Details!"
                }
            };
                            return JsonConvert.SerializeObject(Data);
                        }
                        var generatedToken = await _tokenService.CreateToken(userdata, 7 * 24 * 60);

                        var responseData = new
                        {
                            status = 200,
                            Username = userdata.Email.ToString().Split(".")[0],
                            Token = generatedToken,
                            UserId = userdata.Id,
                            Email = userdata.Email,
                            Department = userdata.Department,
                            Grade= userdata.Grade,
                            ProfileImage = !string.IsNullOrEmpty(userdata.ProfileImage) ? _imageService.ConvertLocalImageToBase64(userdata.ProfileImage) : null,
                            WallPaperDetails = await (from w in _context.wallpaper
                                                      where w.UserId == userdata.Id
                                                      select new WallpaperDto
                                                      {
                                                          WallpaperId = w.WallpaperId,
                                                          WallpaperName = w.WallpaperName,
                                                          WallpaperPath = !string.IsNullOrEmpty(w.WallpaperPath) ? _imageService.ConvertLocalImageToBase64(w.WallpaperPath) : null,
                                                          UserId = w.UserId,

                                                      }).FirstOrDefaultAsync(),
                            IsMoodFilled = await (from w in _context.MoodToday
                                                  where w.UserId == userdata.Id && w.ReportedDateTime.Date == DateTime.Now.Date
                                                  orderby w.MoodId descending
                                                  select new MoodTodayDto
                                                  {
                                                      ReportedDateTime = w.ReportedDateTime,
                                                      Comment = w.Comment,
                                                      MoodId = w.MoodId,
                                                      Rating = w.Rating,
                                                  }).FirstOrDefaultAsync()

                        };
                        return Ok(JsonConvert.SerializeObject(responseData));

                    }

                }
                else
                {

                    return new
                    {
                        status = 400,
                        Message = "Invalid TicketNo Details"
                    };
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }








    }
}
