using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface ICanteenMenuRepository
    {
         Task<bool> SaveAllAsync();
        void AddCanteenMenus(CanteenMenus canteen);
        Task<CanteenMenuListDto> CanteenMenuList(DateTime Date, string Location);
       
    }
}