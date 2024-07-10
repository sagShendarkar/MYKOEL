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
      IConfiguration configuration, ITokenService tokenService, AssetDetails assetDetails, IMapper mapper, IUserRepository userRepository,IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _assetDetails = assetDetails;
            _mapper = mapper;
            _imageService=imageService;

        }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(LoginDto loginDto)
  {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
      if (user == null)
      {
          var Data = new[]
          {
              new { status = 400, ErrorMessage = "Invalid Username!" }
             };
          return JsonConvert.SerializeObject(Data);
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
                            Username = usermodel.UserName,
                            Token = generatedToken,
                            UserId = usermodel.Id,
                            ProfileImage=!string.IsNullOrEmpty(usermodel.ProfileImage)? _imageService.ConvertLocalImageToBase64(usermodel.ProfileImage): null,
                            WallPaperDetails=await( from w in _context.wallpaper
                                               where w.UserId == usermodel.Id
                                               select new WallpaperDto{
                                                WallpaperId=w.WallpaperId,
                                                WallpaperName=w.WallpaperName,
                                                WallpaperPath= !string.IsNullOrEmpty(w.WallpaperPath) ? _imageService.ConvertLocalImageToBase64(w.WallpaperPath) : null,
                                             UserId=w.UserId
                                               }).FirstOrDefaultAsync()
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
                            Username = userdata.UserName,
                            Token = generatedToken,
                            UserId = userdata.Id,
                            ProfileImage=!string.IsNullOrEmpty(userdata.ProfileImage)? _imageService.ConvertLocalImageToBase64(userdata.ProfileImage ): null,
                            WallPaperDetails=await( from w in _context.wallpaper
                                               where w.UserId == userdata.Id
                                               select new WallpaperDto{
                                                WallpaperId=w.WallpaperId,
                                                WallpaperName=w.WallpaperName,
                                                WallpaperPath= !string.IsNullOrEmpty(w.WallpaperPath) ? _imageService.ConvertLocalImageToBase64(w.WallpaperPath) : null,
                                                UserId=w.UserId,
                                           
                                               }).FirstOrDefaultAsync()
                 
                        };
                        return Ok(JsonConvert.SerializeObject(responseData));

                    }

      var usersData = new[]
      {
          new
          {
              status = 200,
              Username = user.UserName,
              UserId = user.Id,
              Token = generatedToken
          }
      };



    }
}
