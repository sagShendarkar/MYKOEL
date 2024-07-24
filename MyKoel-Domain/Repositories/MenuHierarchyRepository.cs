using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using iot_Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Master;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Repositories
{
    public class MenuHierarchyRepository : IMenuHierarchyRepository
    {
        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public MenuHierarchyRepository(DataContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }


        public async Task<List<MainMenuGroupDto>> GetMenuData(int UserId, string Flag, string Grade)
        {
            try
            {

                if (Grade == "SysAdmin")
                {
                    var mainmenuData = await (from menu in _context.MainMenuGroups
                                              where menu.IsActive == true
                                              group menu by new { menu.MainMenuGroupId } into grouped
                                              select new MainMenuGroupDto
                                              {
                                                  MainMenuGroupId = grouped.Key.MainMenuGroupId,
                                                  MenuGroupName = grouped.FirstOrDefault().MenuGroupName,
                                                  Icon = grouped.FirstOrDefault().Icon,
                                                  Sequence = grouped.FirstOrDefault().Sequence,
                                                  IsActive = grouped.FirstOrDefault().IsActive,
                                                  Route = grouped.FirstOrDefault().Route,
                                                  Flag = grouped.FirstOrDefault().Flag,
                                                  ImageIcon = grouped.FirstOrDefault().ImageIcon,
                                                  IsIcon = grouped.FirstOrDefault().IsIcon,
                                                  IsImage = grouped.FirstOrDefault().IsImage,
                                                  IsPopup = grouped.FirstOrDefault().IsPopup,
                                                  IsRoute = grouped.FirstOrDefault().IsRoute,
                                                  IsChild = grouped.FirstOrDefault().IsChild,
                                                  MenuGroupData = (from mg in _context.MenuGroups
                                                                   where mg.MainMenuGroupId == grouped.Key.MainMenuGroupId
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
                                                                                    where mainmenu.MenuGroupId == MenuGroupData.FirstOrDefault().MenuGroupId
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
                        if (Flag == "Top MenuBar")
                        {
                            mainmenuData = mainmenuData.Where(s => s.Flag == "Top MenuBar" || s.Flag == "Admin Menu").ToList();
                        }
                        else
                        {
                            mainmenuData = mainmenuData.Where(s => s.Flag == Flag).ToList();
                        }
                    }
                    foreach (var item in mainmenuData)
                    {
                        if (!string.IsNullOrEmpty(item.ImageIcon))
                        {
                            item.ImageIcon = _imageService.ConvertLocalImageToBase64(item.ImageIcon);
                        }
                    }
                    return mainmenuData;

                }

                var menuData = await (from menu in _context.MainMenuGroups
                                      join u in _context.UserMenuMap
                                      on menu.MainMenuGroupId equals u.MainMenuGroupId
                                      where u.UserId == UserId && menu.IsActive == true
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
                                          ImageIcon = grouped.FirstOrDefault().ImageIcon,
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
                    if (Flag == "Top MenuBar")
                    {
                        menuData = menuData.Where(s => s.Flag == "Top MenuBar" || s.Flag == "Admin Menu").ToList();
                    }
                    else
                    {
                        menuData = menuData.Where(s => s.Flag == Flag).ToList();
                    }
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

        public async Task<List<MenuHierarchyDto>> GetMenuList(string? Name)
        {
            var mainMenuData = await (from mainmenu in _context.MenuMaster
                                      where mainmenu.Flag == "Admin Menu"
                                      select new MenuHierarchyDto
                                      {
                                          MenusId = mainmenu.MenusId,
                                          MenuName = mainmenu.MenuName,
                                          Level=mainmenu.Level,
                                          ParentId=mainmenu.ParentId,
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
                                          IsChild = mainmenu.IsChild
                                      }).ToListAsync();
            if (!string.IsNullOrEmpty(Name))
            {
                mainMenuData = mainMenuData.Where(s => s.MenuName.ToLower().Contains(Name.ToLower())).ToList();
            }
            return mainMenuData;
        }

        public void UpdateMainMenu(MenuMaster mainMenu)
        {
            _context.Entry(mainMenu).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<MenuMaster> GetMainMenuById(int Id)
        {
            var Mainmenu = await _context.MenuMaster
            .SingleOrDefaultAsync(x => x.MenusId == Id);
            return Mainmenu;
        }

        public void AddNewMainMenu(MenuMaster mainMenu)
        {
            _context.Entry(mainMenu).State = EntityState.Added;
        }

        public async Task<MenuHierarchyDto> GetMainMenuDetails(int MainMenuId)
        {
            var Mainmenu = await (from m in _context.MenuMaster
                                  where m.MenusId == MainMenuId
                                  select new MenuHierarchyDto
                                  {
                                      MenusId = m.MenusId,
                                      MenuName = m.MenuName,
                                      Sequence = m.Sequence,
                                      Level=m.Level,
                                      ParentId=m.ParentId,
                                      Icon = m.Icon,
                                      IsActive = m.IsActive,
                                      IsChild = m.IsChild,
                                      Route = m.Route,
                                      IsImage = m.IsImage,
                                      IsRoute = m.IsRoute,
                                      IsIcon = m.IsIcon,
                                      ImageIcon=m.ImageIcon,
                                      ImageBase64 = !string.IsNullOrEmpty(m.ImageIcon) ? _imageService.ConvertLocalImageToBase64(m.ImageIcon) : null,
                                      Flag = m.Flag
                                  }).SingleOrDefaultAsync();
            return Mainmenu;
        }

        public async Task<PagedList<MenuHierarchyDto>> GetMainMenuList(ParameterParams parameterParams)
        {
            var Mainmenu = (from m in _context.MenuMaster
                            where m.IsActive == true && (m.Flag.ToLower() == ("Quick Links").ToLower())
                            select new MenuHierarchyDto
                            {
                                MenusId = m.MenusId,
                                MenuName = m.MenuName,
                                Sequence = m.Sequence,
                                Icon = m.Icon,
                                Level=m.Level,
                                ParentId=m.ParentId,
                                IsActive = m.IsActive,
                                IsChild = m.IsChild,
                                Route = m.Route,
                                IsImage = m.IsImage,
                                IsRoute = m.IsRoute,
                                IsIcon = m.IsIcon,
                                ImageIcon = !string.IsNullOrEmpty(m.ImageIcon) ? _imageService.ConvertLocalImageToBase64(m.ImageIcon) : null,
                                Flag = m.Flag
                            }).AsQueryable();
            return await PagedList<MenuHierarchyDto>.CreateAsync(Mainmenu.ProjectTo<MenuHierarchyDto>(_mapper.ConfigurationProvider)
                                  .AsNoTracking(), parameterParams.PageNumber, parameterParams.PageSize);
        }

        public void DeleteMainMenu(MenuMaster mainMenu)
        {
            mainMenu.IsActive = false;
            _context.Entry(mainMenu).State = EntityState.Modified;

        }


        public async Task<List<MenuMasterDto>> GetMenu4thLevelsData(int UserId, string? Flag)
        {
            var menuData = await (from menu in _context.MenuMaster
                                    join u in _context.UserMenuMap
                                    on menu.MenusId equals u.MenusId
                                    where u.UserId == UserId && menu.MenusId== u.MenusId && menu.IsActive == true
                                  where menu.Level == 1 && (!string.IsNullOrEmpty(Flag) ? menu.Flag==Flag :( menu.Flag=="Top MenuBar" || menu.Flag=="Admin Menu"))  && menu.IsActive==true
                                  group menu by new { menu.MenusId } into grouped
                                  select new MenuMasterDto
                                  {
                                      MenusId = grouped.Key.MenusId,
                                      MenuName = grouped.FirstOrDefault().MenuName,
                                      Icon = grouped.FirstOrDefault().Icon,
                                      Sequence = grouped.FirstOrDefault().Sequence,
                                      IsActive = grouped.FirstOrDefault().IsActive,
                                      Route = grouped.FirstOrDefault().Route,
                                      Flag = grouped.FirstOrDefault().Flag,
                                      ImageIcon = grouped.FirstOrDefault().ImageIcon,
                                      IsIcon = grouped.FirstOrDefault().IsIcon,
                                      IsImage = grouped.FirstOrDefault().IsImage,
                                      IsPopup = grouped.FirstOrDefault().IsPopup,
                                      IsRoute = grouped.FirstOrDefault().IsRoute,
                                      IsChild = grouped.FirstOrDefault().IsChild,
                                      SecondLevelMenuList = (from mg in _context.MenuMaster
                                                            join u in _context.UserMenuMap
                                                             on mg.MenusId equals u.MenusId
                                                             where  u.UserId == UserId && mg.MenusId == u.MenusId && mg.ParentId == grouped.FirstOrDefault().MenusId 
                                                             && mg.ParentId != null && mg.Level == 2 
                                                             group mg by new {mg.MenusId } into MenuGroupData
                                                             select new SecondLevelMenu
                                                             {
                                                                 MenusId = MenuGroupData.FirstOrDefault().MenusId,
                                                                 MenuName = MenuGroupData.FirstOrDefault().MenuName,
                                                                 ParentId = MenuGroupData.FirstOrDefault().ParentId,
                                                                 Level = MenuGroupData.FirstOrDefault().Level,
                                                                 Icon = MenuGroupData.FirstOrDefault().Icon,
                                                                 Sequence = MenuGroupData.FirstOrDefault().Sequence,
                                                                 IsActive = MenuGroupData.FirstOrDefault().IsActive,
                                                                 Route = MenuGroupData.FirstOrDefault().Route,
                                                                 Flag = MenuGroupData.FirstOrDefault().Flag,
                                                                 ImageIcon = MenuGroupData.FirstOrDefault().ImageIcon,
                                                                 IsIcon = MenuGroupData.FirstOrDefault().IsIcon,
                                                                 IsImage = MenuGroupData.FirstOrDefault().IsImage,
                                                                 IsPopup = MenuGroupData.FirstOrDefault().IsPopup,
                                                                 IsRoute = MenuGroupData.FirstOrDefault().IsRoute,
                                                                 IsChild = MenuGroupData.FirstOrDefault().IsChild,
                                                                 ThirdLevelMenuList = (from mg in _context.MenuMaster
                                                                                       join u in _context.UserMenuMap
                                                                                         on mg.MenusId equals u.MenusId
                                                                                       where  u.UserId == UserId && mg.MenusId == u.MenusId &&
                                                                                       mg.ParentId == MenuGroupData.FirstOrDefault().MenusId && mg.ParentId != null && mg.Level == 3
                                                                                       group mg by new { mg.MenusId } into submenudata
                                                                                       select new ThirdLevelMenu
                                                                                       {
                                                                                           MenusId = submenudata.FirstOrDefault().MenusId,
                                                                                           MenuName = submenudata.FirstOrDefault().MenuName,
                                                                                           ParentId = submenudata.FirstOrDefault().ParentId,
                                                                                           Level = submenudata.FirstOrDefault().Level,
                                                                                           Icon = submenudata.FirstOrDefault().Icon,
                                                                                           Sequence = submenudata.FirstOrDefault().Sequence,
                                                                                           IsActive = submenudata.FirstOrDefault().IsActive,
                                                                                           Route = submenudata.FirstOrDefault().Route,
                                                                                           Flag = submenudata.FirstOrDefault().Flag,
                                                                                           ImageIcon = submenudata.FirstOrDefault().ImageIcon,
                                                                                           IsIcon = submenudata.FirstOrDefault().IsIcon,
                                                                                           IsImage = submenudata.FirstOrDefault().IsImage,
                                                                                           IsPopup = submenudata.FirstOrDefault().IsPopup,
                                                                                           IsRoute = submenudata.FirstOrDefault().IsRoute,
                                                                                           IsChild = submenudata.FirstOrDefault().IsChild,
                                                                                           FourthLevelMenuList = (from mg in _context.MenuMaster
                                                                                                                  join u in _context.UserMenuMap
                                                                                                                   on mg.MenusId equals u.MenusId
                                                                                                                    where  u.UserId == UserId && mg.MenusId == u.MenusId &&
                                                                                                                     mg.ParentId == submenudata.FirstOrDefault().MenusId && mg.ParentId != null && mg.Level == 4
                                                                                                                  group mg by new { mg.MenusId } into childMenu

                                                                                                                  select new FourthLevelMenu
                                                                                                                  {
                                                                                                                      MenusId = childMenu.FirstOrDefault().MenusId,
                                                                                                                      MenuName = childMenu.FirstOrDefault().MenuName,
                                                                                                                      ParentId = childMenu.FirstOrDefault().ParentId,
                                                                                                                      Level = childMenu.FirstOrDefault().Level,
                                                                                                                      Icon = childMenu.FirstOrDefault().Icon,
                                                                                                                      Sequence = childMenu.FirstOrDefault().Sequence,
                                                                                                                      IsActive = childMenu.FirstOrDefault().IsActive,
                                                                                                                      Route = childMenu.FirstOrDefault().Route,
                                                                                                                      Flag = childMenu.FirstOrDefault().Flag,
                                                                                                                      ImageIcon = childMenu.FirstOrDefault().ImageIcon,
                                                                                                                      IsIcon = childMenu.FirstOrDefault().IsIcon,
                                                                                                                      IsImage = childMenu.FirstOrDefault().IsImage,
                                                                                                                      IsPopup = childMenu.FirstOrDefault().IsPopup,
                                                                                                                      IsRoute = childMenu.FirstOrDefault().IsRoute,
                                                                                                                      IsChild = childMenu.FirstOrDefault().IsChild,
                                                                                                                  }).OrderBy(a => a.Sequence).ToList()

                                                                                       }).OrderBy(a => a.Sequence).ToList()
                                                             }).ToList()
                                  }).OrderBy(s => s.MenusId).ToListAsync();

            if (!string.IsNullOrEmpty(Flag))
            {
                if (Flag == "Top MenuBar")
                {
                    menuData = menuData.Where(s => s.Flag == "Top MenuBar" || s.Flag == "Admin Menu").ToList();
                }
                else
                {
                    menuData = menuData.Where(s => s.Flag == Flag).ToList();
                }
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

        public async Task<List<MenuMasterDto>> GetMenuLevelsForSysAdmin(int UserId, string? Flag)
        {
              var menuData = await (from menu in _context.MenuMaster
                                  where menu.Level == 1 && (!string.IsNullOrEmpty(Flag) ? menu.Flag==Flag :( menu.Flag=="Top MenuBar" || menu.Flag=="Admin Menu"))  && menu.IsActive==true
                                  group menu by new { menu.MenusId } into grouped
                                  select new MenuMasterDto
                                  {
                                      MenusId = grouped.Key.MenusId,
                                      MenuName = grouped.FirstOrDefault().MenuName,
                                      Icon = grouped.FirstOrDefault().Icon,
                                      Sequence = grouped.FirstOrDefault().Sequence,
                                      IsActive = grouped.FirstOrDefault().IsActive,
                                      Route = grouped.FirstOrDefault().Route,
                                      Flag = grouped.FirstOrDefault().Flag,
                                      ImageIcon = grouped.FirstOrDefault().ImageIcon,
                                      IsIcon = grouped.FirstOrDefault().IsIcon,
                                      IsImage = grouped.FirstOrDefault().IsImage,
                                      IsPopup = grouped.FirstOrDefault().IsPopup,
                                      IsRoute = grouped.FirstOrDefault().IsRoute,
                                      IsChild = grouped.FirstOrDefault().IsChild,
                                      SecondLevelMenuList = (from mg in _context.MenuMaster
                                                            where   mg.ParentId == grouped.FirstOrDefault().MenusId 
                                                             && mg.ParentId != null && mg.Level == 2 
                                                             group mg by new {mg.MenusId } into MenuGroupData
                                                             select new SecondLevelMenu
                                                            {
                                                                 MenusId = MenuGroupData.FirstOrDefault().MenusId,
                                                                 MenuName = MenuGroupData.FirstOrDefault().MenuName,
                                                                 ParentId = MenuGroupData.FirstOrDefault().ParentId,
                                                                 Level = MenuGroupData.FirstOrDefault().Level,
                                                                 Icon = MenuGroupData.FirstOrDefault().Icon,
                                                                 Sequence = MenuGroupData.FirstOrDefault().Sequence,
                                                                 IsActive = MenuGroupData.FirstOrDefault().IsActive,
                                                                 Route = MenuGroupData.FirstOrDefault().Route,
                                                                 Flag = MenuGroupData.FirstOrDefault().Flag,
                                                                 ImageIcon = MenuGroupData.FirstOrDefault().ImageIcon,
                                                                 IsIcon = MenuGroupData.FirstOrDefault().IsIcon,
                                                                 IsImage = MenuGroupData.FirstOrDefault().IsImage,
                                                                 IsPopup = MenuGroupData.FirstOrDefault().IsPopup,
                                                                 IsRoute = MenuGroupData.FirstOrDefault().IsRoute,
                                                                 IsChild = MenuGroupData.FirstOrDefault().IsChild,
                                                                 ThirdLevelMenuList = (from mg in _context.MenuMaster
                                                                                       where   mg.ParentId == MenuGroupData.FirstOrDefault().MenusId && mg.ParentId != null && mg.Level == 3
                                                                                       group mg by new { mg.MenusId } into submenudata
                                                                                       select new ThirdLevelMenu
                                                                                       {
                                                                                           MenusId = submenudata.FirstOrDefault().MenusId,
                                                                                           MenuName = submenudata.FirstOrDefault().MenuName,
                                                                                           ParentId = submenudata.FirstOrDefault().ParentId,
                                                                                           Level = submenudata.FirstOrDefault().Level,
                                                                                           Icon = submenudata.FirstOrDefault().Icon,
                                                                                           Sequence = submenudata.FirstOrDefault().Sequence,
                                                                                           IsActive = submenudata.FirstOrDefault().IsActive,
                                                                                           Route = submenudata.FirstOrDefault().Route,
                                                                                           Flag = submenudata.FirstOrDefault().Flag,
                                                                                           ImageIcon = submenudata.FirstOrDefault().ImageIcon,
                                                                                           IsIcon = submenudata.FirstOrDefault().IsIcon,
                                                                                           IsImage = submenudata.FirstOrDefault().IsImage,
                                                                                           IsPopup = submenudata.FirstOrDefault().IsPopup,
                                                                                           IsRoute = submenudata.FirstOrDefault().IsRoute,
                                                                                           IsChild = submenudata.FirstOrDefault().IsChild,
                                                                                           FourthLevelMenuList = (from mg in _context.MenuMaster
                                                                                                                  where  mg.ParentId == submenudata.FirstOrDefault().MenusId && mg.ParentId != null && mg.Level == 4
                                                                                                                  group mg by new { mg.MenusId } into childMenu

                                                                                                                  select new FourthLevelMenu
                                                                                                                  {
                                                                                                                      MenusId = childMenu.FirstOrDefault().MenusId,
                                                                                                                      MenuName = childMenu.FirstOrDefault().MenuName,
                                                                                                                      ParentId = childMenu.FirstOrDefault().ParentId,
                                                                                                                      Level = childMenu.FirstOrDefault().Level,
                                                                                                                      Icon = childMenu.FirstOrDefault().Icon,
                                                                                                                      Sequence = childMenu.FirstOrDefault().Sequence,
                                                                                                                      IsActive = childMenu.FirstOrDefault().IsActive,
                                                                                                                      Route = childMenu.FirstOrDefault().Route,
                                                                                                                      Flag = childMenu.FirstOrDefault().Flag,
                                                                                                                      ImageIcon = childMenu.FirstOrDefault().ImageIcon,
                                                                                                                      IsIcon = childMenu.FirstOrDefault().IsIcon,
                                                                                                                      IsImage = childMenu.FirstOrDefault().IsImage,
                                                                                                                      IsPopup = childMenu.FirstOrDefault().IsPopup,
                                                                                                                      IsRoute = childMenu.FirstOrDefault().IsRoute,
                                                                                                                      IsChild = childMenu.FirstOrDefault().IsChild,
                                                                                                                  }).OrderBy(a => a.Sequence).ToList()

                                                                                       }).OrderBy(a => a.Sequence).ToList()
                                                            }).ToList()
                                  }).OrderBy(s => s.MenusId).ToListAsync();

            
            foreach (var item in menuData)
            {
                if (!string.IsNullOrEmpty(item.ImageIcon))
                {
                    item.ImageIcon = _imageService.ConvertLocalImageToBase64(item.ImageIcon);
                }
            }

            return menuData;

        }

        public void UpdateMainMenu(Migrations.MenuMaster mainMenu)
        {
            throw new NotImplementedException();
        }

    }
}