using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using iot_Domain.Helpers;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Interfaces
{
    public interface IMenuHierarchyRepository
    {
        Task<List<MainMenuGroupDto>> GetMenuData(int UserId, string Flag, string Grade);
        Task<List<MainMenuGroupDto>> GetMenuList(string Name);
        void UpdateMainMenu(MainMenuGroup mainMenu);
        Task<bool> SaveAllAsync();
        Task<MainMenuGroup> GetMainMenuById(int Id);
        void AddNewMainMenu(MainMenuGroup mainMenu);  
        Task<AddMainMenuGroupDto> GetMainMenuDetails(int MainMenuId);
        Task<PagedList<AddMainMenuGroupDto>> GetMainMenuList(ParameterParams parameterParams);
        void DeleteMainMenu(MainMenuGroup mainMenu);

    }
}