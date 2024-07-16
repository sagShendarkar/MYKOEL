using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Data;
using MyKoel_Domain.Extensions;
namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuHierarchyController : ControllerBase
    {
        private readonly DataContext _context;
         private readonly IMenuHierarchyRepository _menuHierarchy;

        public MenuHierarchyController(DataContext context,IMenuHierarchyRepository menuHierarchy)
        {
            _context = context;
            _menuHierarchy=menuHierarchy;
        }

        [HttpGet("ShowMenuList")]
        public async Task<ActionResult<IEnumerable<MainMenuGroupDto>>> GetMenuList(string? Flag, string? Grade)
        {
            
            var UserId=User.GetUserId();
            var menu=await _menuHierarchy.GetMenuData(UserId,Flag,Grade);
            return menu;
        }
        
        [HttpGet("MenuList")]
        public async Task<List<MainMenuGroupDto>> MenuList(string? Name)
        {
            var menu=await _menuHierarchy.GetMenuList(Name);
            return menu;
        }

    }
}