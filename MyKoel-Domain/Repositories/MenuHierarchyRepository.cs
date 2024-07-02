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

        public async Task<List<MenusDto>> GetMenuData(int UserId)
        {
            var menuData = (from menu in _context.Menus
                            join u in _context.UserMenuMap
                            on menu.MenuId equals u.MenuId
                            where u.UserId == UserId
                           select new MenusDto{
                             MenuId=menu.MenuId,
                             MenuName=menu.MenuName,
                             Icon=menu.Icon,
                             Sequence=menu.Sequence,
                             IsActive=menu.IsActive,
                             Route=menu.Route,
                             MenuGroupId=menu.MenuGroupId,
                             MenuGroupData= (from mg in _context.MenuGroups
                                            where mg.MenuGroupId==menu.MenuGroupId
                                             select new MenuGroupDto
                                             { 
                                                 MenuGroupId=mg.MenuGroupId,
                                                 MainMenuGroupId=mg.MainMenuGroupId,
                                                 GroupName=mg.GroupName,
                                                 Sequence=mg.Sequence,
                                                 Icon=mg.Icon,
                                                 IsActive=mg.IsActive,
                                                 IsChild=mg.IsChild,
                                                 Route=mg.Route ,
                                                 MainMenuData= (from mainmenu in _context.MainMenuGroups
                                                                where mg.MainMenuGroupId==mainmenu.MainMenuGroupId
                                                    select new MainMenuGroupDto
                                                    { 
                                                        MainMenuGroupId=mainmenu.MainMenuGroupId,
                                                        MenuGroupName=mainmenu.MenuGroupName,
                                                        Sequence=mainmenu.Sequence,
                                                        Icon=mainmenu.Icon,
                                                        IsActive=mainmenu.IsActive,
                                                        IsChild=mainmenu.IsChild,
                                                        Route=mainmenu.Route 
                                                    }).OrderBy(a=>a.Sequence).ToList()

                                             }).OrderBy(a=>a.Sequence).ToList(),

                           }).ToList();
            return menuData;
        }
    }
}