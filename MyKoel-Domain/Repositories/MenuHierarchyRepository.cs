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
        public MenuHierarchyRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper; 
        }

        public async Task<List<MainMenuGroupDto>> GetMenuData(int UserId)
        {
          var menuData = (from menu in _context.MainMenuGroups
                join u in _context.UserMenuMap
                on menu.MainMenuGroupId equals u.MainMenuGroupId
                where u.UserId == UserId
                group menu by new {menu.MainMenuGroupId, u.UserId} into grouped
                select new MainMenuGroupDto
                {
                    MainMenuGroupId = grouped.Key.MainMenuGroupId,
                    MenuGroupName = grouped.FirstOrDefault().MenuGroupName,
                    Icon = grouped.FirstOrDefault().Icon,
                    Sequence = grouped.FirstOrDefault().Sequence,
                    IsActive = grouped.FirstOrDefault().IsActive,
                    Route = grouped.FirstOrDefault().Route,
                    Flag=grouped.FirstOrDefault().Flag,
                    MenuGroupData = (from mg in _context.MenuGroups
                                      join u in _context.UserMenuMap
                                    on mg.MenuGroupId equals u.MenuGroupId
                                     where u.UserId == UserId
                                     group mg by new {mg.MenuGroupId, u.UserId} into MenuGroupData
                                     where MenuGroupData.FirstOrDefault().MainMenuGroupId == grouped.Key.MainMenuGroupId
                                      &&  grouped.Key.UserId == UserId
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
                                                      && um.MenuId==mainmenu.MenuId
                                                      && um.MenuGroupId==MenuGroupData.FirstOrDefault().MenuGroupId
                                                      select new MenusDto
                                                      {
                                                          MenuId = mainmenu.MenuId,
                                                          MenuName = mainmenu.MenuName,
                                                          Sequence = mainmenu.Sequence,
                                                          Icon = mainmenu.Icon,
                                                          IsActive = mainmenu.IsActive,
                                                          Route = mainmenu.Route
                                                      }).OrderBy(a => a.Sequence).ToList()
                                     }).OrderBy(a => a.Sequence).ToList(),
                                 }).OrderBy(s=>s.MainMenuGroupId).ToList();
           return menuData;
        }

        // public async Task<List<MainMenuGroupDto>> GetWallpaperData(int UserId)
        // {

        //      var menuData = await (from menu in _context.MainMenuGroups
        //                      where menu.Flag.ToLower()=="Wallpaper Menus".ToLower()
        //         // join u in _context.UserMenuMap
        //         // on menu.MainMenuGroupId equals u.MainMenuGroupId
        //         // where u.UserId == UserId
        //         // group menu by new {menu.MainMenuGroupId, u.UserId} into grouped
        //         select new MainMenuGroupDto
        //         {
        //             MainMenuGroupId = menu.MainMenuGroupId,
        //             MenuGroupName = menu.MenuGroupName,
        //             Icon = menu.Icon,
        //             Sequence = menu.Sequence,
        //             IsActive = menu.IsActive,
        //             Route = menu.Route,
        //             Flag=menu.Flag,
        //             ImageIcon=menu.ImageIcon,
        //             IsIcon=menu.IsIcon,
        //             IsImage=menu.IsImage,
        //             IsPopup=menu.IsPopup,
        //             IsRoute=menu.IsRoute,
        //             IsChild=menu.IsChild
        //         }).ToListAsync();
        //         return menuData;  
        // }

        public async Task<List<MainMenuGroupDto>> GetWallpaperData(int UserId,string Flag)
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
                    Flag=menu.Flag,
                    ImageIcon=menu.ImageIcon,
                    IsIcon=menu.IsIcon,
                    IsImage=menu.IsImage,
                    IsPopup=menu.IsPopup,
                    IsRoute=menu.IsRoute,
                    IsChild=menu.IsChild
                }).ToListAsync();
           return menuData;
        }
    }
}