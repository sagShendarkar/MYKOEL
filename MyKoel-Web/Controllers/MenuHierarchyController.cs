using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Data;
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
        public async Task<ActionResult<IEnumerable<MainMenuGroupDto>>> GetMenuList(int UserId)
        {
            var menu=await _menuHierarchy.GetMenuData(UserId);
            return menu;
        }

    }
}