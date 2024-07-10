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


  public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context, 
      IConfiguration configuration, ITokenService tokenService)
  {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
      _context = context;
      _configuration = configuration;
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
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

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
      if (!result.Succeeded) {
          var Data = new[]
          {
              new { status = 400, ErrorMessage = "Invalid Login Details!" }
             };
          return JsonConvert.SerializeObject(Data);
      }  

      var generatedToken = await _tokenService.CreateToken(user, 7 * 24 * 60);


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

      return JsonConvert.SerializeObject(usersData);

  }

    }
}
