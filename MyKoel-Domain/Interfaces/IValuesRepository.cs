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
    public interface IValuesRepository
    {
        void UpdateValues(ValuesMaster values);
        Task<bool> SaveAllAsync();
        void DeleteValues(ValuesMaster values);
        Task<ValuesMaster> GetValuesById(int Id);
        void AddNewValues(ValuesMaster values);
        Task<PagedList<ValuesDto>> GetValuesList(ParameterParams parameterParams);
        Task<ValuesDto> GetValuesDetailById(int Id);

    }
}