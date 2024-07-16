using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface IHolidayCalenderRepository
    {
        Task<List<HolidayCalendar>> HolidayCalendarList(string Location);
        Task<object> HolidayExcelUpload(UploadExcelDto uploadExcel);
        Task<List<LocationDto>> LocationList(string Location);

    }
}