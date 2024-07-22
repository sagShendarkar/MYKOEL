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
                                              where menu.IsActive==true
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
                                                  MenuGroupData =(from mg in _context.MenuGroups
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
                                      where u.UserId == UserId && menu.IsActive==true
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

        public async Task<List<MainMenuGroupDto>> GetMenuList(string? Name)
        {
            var mainMenuData = await (from mainmenu in _context.MainMenuGroups
                                      where mainmenu.Flag == "Admin Menu"
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

        public void UpdateMainMenu(MainMenuGroup mainMenu)
        {
            _context.Entry(mainMenu).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<MainMenuGroup> GetMainMenuById(int Id)
        {
            var Mainmenu = await _context.MainMenuGroups
            .SingleOrDefaultAsync(x => x.MainMenuGroupId == Id);
            return Mainmenu;
        }

        public void AddNewMainMenu(MainMenuGroup mainMenu)
        {
            _context.Entry(mainMenu).State = EntityState.Added;
        }

        public async Task<AddMainMenuGroupDto> GetMainMenuDetails(int MainMenuId)
        {
            var Mainmenu = await (from m in _context.MainMenuGroups
                                  where m.MainMenuGroupId == MainMenuId
                                  select new AddMainMenuGroupDto
                                  {
                                      MainMenuGroupId = m.MainMenuGroupId,
                                      MenuGroupName = m.MenuGroupName,
                                      Sequence = m.Sequence,
                                      Icon = m.Icon,
                                      IsActive = m.IsActive,
                                      IsChild = m.IsChild,
                                      Route = m.Route,
                                      IsImage = m.IsImage,
                                      IsRoute = m.IsRoute,
                                      IsIcon = m.IsIcon,
                                      ImageIcon = !string.IsNullOrEmpty(m.ImageIcon) ? _imageService.ConvertLocalImageToBase64(m.ImageIcon) : null,
                                      Flag = m.Flag
                                  }).SingleOrDefaultAsync();
            return Mainmenu;
        }

        public async Task<PagedList<AddMainMenuGroupDto>> GetMainMenuList(ParameterParams parameterParams)
        {
            var Mainmenu = (from m in _context.MainMenuGroups
                            where m.IsActive == true && (m.Flag.ToLower() == ("Quick Links").ToLower())
                            select new AddMainMenuGroupDto
                            {
                                MainMenuGroupId = m.MainMenuGroupId,
                                MenuGroupName = m.MenuGroupName,
                                Sequence = m.Sequence,
                                Icon = m.Icon,
                                IsActive = m.IsActive,
                                IsChild = m.IsChild,
                                Route = m.Route,
                                IsImage = m.IsImage,
                                IsRoute = m.IsRoute,
                                IsIcon = m.IsIcon,
                                ImageIcon = !string.IsNullOrEmpty(m.ImageIcon) ? _imageService.ConvertLocalImageToBase64(m.ImageIcon) : null,
                                Flag = m.Flag
                            }).AsQueryable();
            return await PagedList<AddMainMenuGroupDto>.CreateAsync(Mainmenu.ProjectTo<AddMainMenuGroupDto>(_mapper.ConfigurationProvider)
                                  .AsNoTracking(), parameterParams.PageNumber, parameterParams.PageSize);
        }

        public void DeleteMainMenu(MainMenuGroup mainMenu)
        {
            mainMenu.IsActive = false;
            _context.Entry(mainMenu).State = EntityState.Modified;

        }

        public async Task<List<MenuDataListDto>> GetMenuLevelsData(int UserId, int? MenuId, int? Level)
        {
            var menuData = await (from menu in _context.MenuMaster
                                    //   join u in _context.UserMenuMap
                                    //   on menu.MenusId equals u.MenusId
                                      //where u.UserId == UserId && menu.IsActive==true
                                      where (Level==1) ? menu.Level==Level : true  && (MenuId > 0 ? menu.MenusId == MenuId :true)
                                      group menu by new { menu.MenusId } into grouped
                                      select new MenuDataListDto
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
                                          NextLevelMenus = (from mg in _context.MenuMaster
                                                        //    join u in _context.UserMenuMap
                                                        //    on mg.MenusId equals u.MenusId
                                                           where 
                                                                grouped.Key.MenusId == MenuId && mg.Level==Level
                                                           group mg by new { mg.MenusId } into MenuGroupData
                                                           select new MenuMasterDto
                                                           {
                                                              MenusId = MenuGroupData.Key.MenusId,
                                          MenuName = MenuGroupData.FirstOrDefault().MenuName,
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
                                        }).OrderBy(a => a.Sequence).ToList()
                                      }).OrderBy(s => s.MenusId).ToListAsync();

                // if (!string.IsNullOrEmpty(Flag))
                // {
                //         if (Flag == "Top MenuBar")
                //         {
                //             menuData = menuData.Where(s => s.Flag == "Top MenuBar" || s.Flag == "Admin Menu").ToList();
                //         }
                //         else
                //         {
                //             menuData = menuData.Where(s => s.Flag == Flag).ToList();
                //         }                
                // }
                // foreach (var item in menuData)
                // {
                //     if (!string.IsNullOrEmpty(item.ImageIcon))
                //     {
                //         item.ImageIcon = _imageService.ConvertLocalImageToBase64(item.ImageIcon);
                //     }
                // }

                return menuData;

        }
    }
}