using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
         private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(DataContext context
          , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager )
        {
             _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
             _userManager = userManager;
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


   
        
    }
}