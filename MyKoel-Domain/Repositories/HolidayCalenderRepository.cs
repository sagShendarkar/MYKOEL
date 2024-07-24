using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExcelDataReader;
using iot_Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Repositories
{
    public class HolidayCalenderRepository : IHolidayCalenderRepository, IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HolidayCalenderRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {
            }
            isDisposed = true;
        }

        public async Task<PagedList<HolidayCalenderDto>> HolidayCalendarList(ParameterParams parameterParams)
        {
            var holidaylist = (from h in _context.HolidayCalendars
                                     where (h.ISACTIVE == true)  && (!string.IsNullOrEmpty(parameterParams.Location) ? h.LOCATION.ToLower() == parameterParams.Location.ToLower():true)
                                     select new HolidayCalenderDto
                                     {
                                         HolidayCalendarId = h.HOLIDAYCALENDERID,
                                         Holiday = h.HOLIDAY,
                                         Date = h.DATE,
                                         Day = h.DAY,
                                         Remarks = h.REMARKS,
                                         Locations = h.LOCATION,
                                         Year = h.YEAR,
                                         IsActive = h.ISACTIVE
                                     }).AsQueryable();
                return await PagedList<HolidayCalenderDto>.CreateAsync(holidaylist.ProjectTo<HolidayCalenderDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(),parameterParams.PageNumber,parameterParams.PageSize);
           }

        public async Task<object> HolidayExcelUpload(UploadExcelDto uploadExcel)
        {
            try
            {
                List<string> messages = new List<string>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                if (uploadExcel.Excelfile.Length > 0)
                {
                    Stream stream = new MemoryStream();
                    uploadExcel.Excelfile.CopyTo(stream);
                    IExcelDataReader reader = null;

                    if (uploadExcel.Excelfile.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadExcel.Excelfile.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        messages.Add("This file format is not supported");
                        return new
                        {
                            Status = 400,
                            Message = string.Join("\n", messages)
                        };

                    }
                    var conf = new ExcelDataSetConfiguration
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    DataSet excelRecords = reader.AsDataSet(conf);
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];

                    if (finalRecords.Rows.Count != 0)
                    {
                        List<HolidayCalendar> objList = new List<HolidayCalendar>();

                        if (finalRecords.Columns.Count != 7)
                        {
                            messages.Add("Invalid column length or column name");
                            return new
                            {
                                Status = 400,
                                Message = string.Join("\n", messages)
                            };
                        }
                           var existingholidays = await _context.HolidayCalendars
                               .Where(x => x.LOCATION.ToLower() == uploadExcel.Location.ToLower())
                               .ToListAsync();
                            if (existingholidays != null)
                            {
                                foreach (var holiday in existingholidays)
                                {
                                    holiday.ISACTIVE = false;
                                    _context.Update(holiday);
                                    await _context.SaveChangesAsync();
                                }
                            }


                        for (int i = 0; i < finalRecords.Rows.Count; i++)
                        {
                         

                            if (Convert.ToString(finalRecords.Rows[i]["Holiday"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Holiday"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Date"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Date"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Day"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Day"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Remarks"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Remarks"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Year"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Year"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Active"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Active"]) + " is required";
                            }
                            HolidayCalendar holidaydata = new HolidayCalendar();

                            holidaydata = new HolidayCalendar
                            {
                                HOLIDAY = Convert.ToString(finalRecords.Rows[i]["Holiday"]),
                                DATE = Convert.ToDateTime(finalRecords.Rows[i]["Date"]),
                                DAY = Convert.ToString(finalRecords.Rows[i]["Day"]),
                                REMARKS = Convert.ToString(finalRecords.Rows[i]["Remarks"]),
                                LOCATION = uploadExcel.Location,
                                YEAR = Convert.ToString(finalRecords.Rows[i]["Year"]),
                                ISACTIVE = Convert.ToBoolean(finalRecords.Rows[i]["Active"]),
                                BATCHID = Guid.NewGuid().ToString()
                            };
                            await _context.HolidayCalendars.AddAsync(holidaydata);
                            // }
                        }

                        await _context.SaveChangesAsync();

                    }
                    messages.Add("Data uploaded successfully");
                    return new
                    {
                        Status = 200,
                        Message = string.Join(",", messages)
                    };
                }
                else
                {
                    messages.Add("Empty data not allowed");
                    return new
                    {
                        Status = 400,
                        Message = string.Join("", messages)
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 400,
                    Message = ex.Message
                };
            }



        }

        public async Task<List<LocationDto>> LocationList(string Location)
        {
            var locationlist = await (from h in _context.Users
                                     where !string.IsNullOrEmpty(h.Location) && (!string.IsNullOrEmpty(Location)? h.Location.ToLower() == Location.ToLower():true)
                                     select new LocationDto
                                     {
                                         Locations = h.Location
                                     }).Distinct().ToListAsync();
            return locationlist;
        }
    }
}