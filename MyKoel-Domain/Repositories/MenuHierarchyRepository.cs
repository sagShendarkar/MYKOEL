using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;

namespace MyKoel_Domain.Repositories
{
    public class MenuHierarchyRepository : IMenuHierarchyRepository
    {
        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public MenuHierarchyRepository(DataContext context, IMapper mapper,IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService=imageService;
        }


        public async Task<List<MainMenuGroupDto>> GetMenuData(int UserId, string Flag)
        {
            try
            {
                var menuData = await (from menu in _context.MainMenuGroups
                                      join u in _context.UserMenuMap
                                      on menu.MainMenuGroupId equals u.MainMenuGroupId
                                      where u.UserId == UserId
                                      group menu by new { menu.MainMenuGroupId, u.UserId } into grouped
                                      select new MainMenuGroupDto
                                      {
                                          MainMenuGroupId = grouped.Key.MainMenuGroupId,
                                          MenuGroupName = grouped.FirstOrDefault().MenuGroupName,
                                          Icon = grouped.FirstOrDefault().Icon,
                                          Sequence = grouped.FirstOrDefault().Sequence,
                                          IsActive = grouped.FirstOrDefault().IsActive,
                                          Route = grouped.FirstOrDefault().Route,
                                          Flag = grouped.FirstOrDefault().Flag,
                                          ImageIcon = grouped.FirstOrDefault().ImageIcon, // Assuming ImageIcon is a string containing file path
                                          IsIcon = grouped.FirstOrDefault().IsIcon,
                                          IsImage = grouped.FirstOrDefault().IsImage,
                                          IsPopup = grouped.FirstOrDefault().IsPopup,
                                          IsRoute = grouped.FirstOrDefault().IsRoute,
                                          IsChild = grouped.FirstOrDefault().IsChild,
                                          MenuGroupData = (from mg in _context.MenuGroups
                                                           join u in _context.UserMenuMap
                                                           on mg.MenuGroupId equals u.MenuGroupId
                                                           where u.UserId == UserId
                                                              && u.MenuGroupId == mg.MenuGroupId
                                                              && mg.MainMenuGroupId == grouped.Key.MainMenuGroupId
                                                           group mg by new { mg.MenuGroupId, mg.MainMenuGroupId } into MenuGroupData
                                                           select new MenuGroupDto
                                                           {
                                                               MenuGroupId = MenuGroupData.FirstOrDefault().MenuGroupId,
                                                               MainMenuGroupId = MenuGroupData.FirstOrDefault().MainMenuGroupId,
                                                               GroupName = MenuGroupData.FirstOrDefault().GroupName,
                                                               Sequence = MenuGroupData.FirstOrDefault().Sequence,
                                                               Icon = MenuGroupData.FirstOrDefault().Icon,
                                                               IsActive = MenuGroupData.FirstOrDefault().IsActive,
                                                               IsChild = MenuGroupData.FirstOrDefault().IsChild,
                                                               Route = MenuGroupData.FirstOrDefault().Route,
                                                               MenusData = (from mainmenu in _context.Menus
                                                                            join um in _context.UserMenuMap
                                                                            on mainmenu.MenuId equals um.MenuId
                                                                            where um.UserId == UserId
                                                                               && um.MenuId == mainmenu.MenuId
                                                                               && um.MenuGroupId == MenuGroupData.FirstOrDefault().MenuGroupId
                                                                            select new MenusDto
                                                                            {
                                                                                MenuId = mainmenu.MenuId,
                                                                                MenuName = mainmenu.MenuName,
                                                                                Sequence = mainmenu.Sequence,
                                                                                Icon = mainmenu.Icon,
                                                                                IsActive = mainmenu.IsActive,
                                                                                Route = mainmenu.Route
                                                                            }).OrderBy(a => a.Sequence).ToList()
                                                           }).OrderBy(a => a.Sequence).ToList()
                                      }).OrderBy(s => s.MainMenuGroupId).ToListAsync();

                if (!string.IsNullOrEmpty(Flag))
                {
                    menuData = menuData.Where(s => s.Flag == Flag).ToList();
                }
                foreach (var item in menuData)
                {
                    if (!string.IsNullOrEmpty(item.ImageIcon))
                    {
                        item.ImageIcon = _imageService.ConvertLocalImageToBase64(item.ImageIcon);
                    }
                }

                return menuData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching menu data: {ex.Message}");
                return null;
            }
        }

        public async Task<List<MainMenuGroupDto>> GetWallpaperData(int UserId, string Flag)
        {
            var menuData = await (from menu in _context.MainMenuGroups
                                  join u in _context.UserMenuMap
                                  on menu.MainMenuGroupId equals u.MainMenuGroupId
                                  where menu.Flag.ToLower().Contains(Flag.ToLower())
                                  && u.UserId == UserId
                                  select new MainMenuGroupDto
                                  {
                                      MainMenuGroupId = menu.MainMenuGroupId,
                                      MenuGroupName = menu.MenuGroupName,
                                      Icon = menu.Icon,
                                      Sequence = menu.Sequence,
                                      IsActive = menu.IsActive,
                                      Route = menu.Route,
                                      Flag = menu.Flag,
                                      ImageIcon = menu.ImageIcon,
                                      IsIcon = menu.IsIcon,
                                      IsImage = menu.IsImage,
                                      IsPopup = menu.IsPopup,
                                      IsRoute = menu.IsRoute,
                                      IsChild = menu.IsChild
                                  }).ToListAsync();
            return menuData;
        }

        public async Task<List<MainMenuGroupDto>> GetMenuList(string? Name)
        {
            var mainMenuData = await (from mainmenu in _context.MainMenuGroups
                                      where mainmenu.Flag == "Top MenuBar"
                                      select new MainMenuGroupDto
                                      {
                                          MainMenuGroupId = mainmenu.MainMenuGroupId,
                                          MenuGroupName = mainmenu.MenuGroupName,
                                          Icon = mainmenu.Icon,
                                          Sequence = mainmenu.Sequence,
                                          IsActive = mainmenu.IsActive,
                                          Route = mainmenu.Route,
                                          Flag = mainmenu.Flag,
                                          ImageIcon = mainmenu.ImageIcon,
                                          IsIcon = mainmenu.IsIcon,
                                          IsImage = mainmenu.IsImage,
                                          IsPopup = mainmenu.IsPopup,
                                          IsRoute = mainmenu.IsRoute,
                                          IsChild = mainmenu.IsChild,
                                          MenuGroupData = (from mg in _context.MenuGroups
                                                           where mg.MainMenuGroupId == mainmenu.MainMenuGroupId
                                                           select new MenuGroupDto
                                                           {
                                                               MenuGroupId = mg.MenuGroupId,
                                                               MainMenuGroupId = mg.MainMenuGroupId,
                                                               GroupName = mg.GroupName,
                                                               Sequence = mg.Sequence,
                                                               Icon = mg.Icon,
                                                               IsActive = mg.IsActive,
                                                               IsChild = mg.IsChild,
                                                               Route = mg.Route,
                                                               MenusData = (from menu in _context.Menus
                                                                            where menu.MenuGroupId == mg.MenuGroupId
                                                                            select new MenusDto
                                                                            {
                                                                                MenuId = menu.MenuId,
                                                                                MenuName = menu.MenuName,
                                                                                Sequence = menu.Sequence,
                                                                                Icon = menu.Icon,
                                                                                IsActive = menu.IsActive,
                                                                                Route = menu.Route
                                                                            }).OrderBy(a => a.Sequence).ToList()
                                                           }).OrderBy(a => a.Sequence).ToList()
                                      }).ToListAsync();
            if (!string.IsNullOrEmpty(Name))
            {
                mainMenuData = mainMenuData.Where(s => s.MenuGroupName.ToLower().Contains(Name.ToLower())).ToList();
            }
            return mainMenuData;
        }
    }
}