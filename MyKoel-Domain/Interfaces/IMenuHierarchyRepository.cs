using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.DTOs;

namespace MyKoel_Domain.Interfaces
{
    public interface IMenuHierarchyRepository
    {
        Task<List<MenusDto>> GetMenuData(int UserId);
    }
}