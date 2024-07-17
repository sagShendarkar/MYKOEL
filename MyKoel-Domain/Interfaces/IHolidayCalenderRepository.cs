using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using iot_Domain.Helpers;
using Microsoft.AspNetCore.Http;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface IHolidayCalenderRepository
    {
        Task<PagedList<HolidayCalenderDto>> HolidayCalendarList(ParameterParams parameterParams);
        Task<object> HolidayExcelUpload(UploadExcelDto uploadExcel);
        Task<List<LocationDto>> LocationList(string Location);

    }
}