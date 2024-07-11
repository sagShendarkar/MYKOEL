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
    public interface ILunchRepository
    {
        Task<PagedList<LunchDto>> GetLunchList(ParameterParams parameterParams);
        void UpdateLunch(LunchMaster lunchMaster);
        Task<bool> SaveAllAsync();
        void DeleteLunch(LunchMaster lunchMaster);
        Task<LunchMaster> GetLunchById(int Id);
        void AddNewLunch(LunchMaster lunchMaster);
        Task<bool> LunchExists(string Name);
        Task<List<LunchDto>> GetDropdownList(int LunchId, string? Desc);

    }
}