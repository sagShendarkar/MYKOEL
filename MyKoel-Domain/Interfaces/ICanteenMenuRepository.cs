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
        Task<List<CanteenMenuListDto>> BreakfastList(DateTime Date, string Location);
        Task<List<CanteenMenuListDto>> LunchList(DateTime Date, string Location);

    }
}