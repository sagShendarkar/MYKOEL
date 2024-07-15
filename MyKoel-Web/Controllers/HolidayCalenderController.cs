using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iot_Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayCalenderController : ControllerBase
    {
         private readonly IHolidayCalenderRepository _holidayCalender;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HolidayCalenderController(IHolidayCalenderRepository holidayCalender, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _holidayCalender = holidayCalender;
        }

        [HttpGet("HolidayCalendarList")]
        public async Task<List<HolidayCalendar>> HolidayCalendarList(string? Location)
        {
            var holidayList = await _holidayCalender.HolidayCalendarList(Location);
            return holidayList;
        }
       

        [HttpPost("UploadExcel")]
        public async Task<ActionResult<object>> UploadExcel(IFormFile Excelfile)
        {
            var filedata = await _holidayCalender.HolidayExcelUpload(Excelfile);
            return filedata;
        }

        [HttpGet("LocationList")]
        public async Task<List<LocationDto>> LocationList(string? Location)
        {
            var locationList = await _holidayCalender.LocationList(Location);
            return locationList;
        }

    }
}