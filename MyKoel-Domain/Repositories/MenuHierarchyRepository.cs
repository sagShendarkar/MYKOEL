using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
                group menu by menu.MainMenuGroupId into grouped
                select new MainMenuGroupDto
                {
                    MainMenuGroupId = grouped.Key,
                    MenuGroupName = grouped.FirstOrDefault().MenuGroupName,
                    Icon = grouped.FirstOrDefault().Icon,
                    Sequence = grouped.FirstOrDefault().Sequence,
                    IsActive = grouped.FirstOrDefault().IsActive,
                    Route = grouped.FirstOrDefault().Route,
                    MenuGroupData = (from mg in _context.MenuGroups
                                     where mg.MainMenuGroupId == grouped.Key
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
                                         MenusData = (from mainmenu in _context.Menus
                                                      join um in _context.UserMenuMap
                                                      on mainmenu.MenuId equals um.MenuId
                                                      where um.UserId == UserId 
                                                      && um.MenuId==mainmenu.MenuId
                                                      && um.MenuGroupId==mg.MenuGroupId
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
    }
}