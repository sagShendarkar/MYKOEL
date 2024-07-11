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
    public interface IBreakFastRepository
    {
        void UpdateBreakFast(BreakFast breakfast);
        Task<bool> SaveAllAsync();
        void DeleteBreakfast(BreakFast breakfast);
        Task<BreakFast> GetBreakfastById(int Id);
        void AddNewBreakfast(BreakFast breakfast);
        Task<bool> BreakfastExists(string Name);
        Task<List<BreakFastDto>> GetDropdownList(int BreakFastId, string? Desc);
        Task<PagedList<BreakFastDto>> GetBreakfastList(ParameterParams parameterParams);

    }
}