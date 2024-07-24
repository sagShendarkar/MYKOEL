using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using iot_Domain.Helpers;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface IMenuHierarchyRepository
    {
        Task<List<MainMenuGroupDto>> GetMenuData(int UserId, string Flag, string Grade);
        Task<List<MenuHierarchyDto>> GetMenuList(string Name);
        void UpdateMainMenu(MenuMaster mainMenu);
        Task<bool> SaveAllAsync();
        Task<MenuMaster> GetMainMenuById(int Id);
        void AddNewMainMenu(MenuMaster mainMenu);  
        Task<MenuHierarchyDto> GetMainMenuDetails(int MainMenuId);
        Task<PagedList<MenuHierarchyDto>> GetMainMenuList(ParameterParams parameterParams);
        void DeleteMainMenu(MenuMaster mainMenu);
        Task<List<MenuMasterDto>> GetMenu4thLevelsData(int UserId, string? Flag);
        Task<List<MenuMasterDto>> GetMenuLevelsForSysAdmin(int UserId,string? Flag);

    }
}