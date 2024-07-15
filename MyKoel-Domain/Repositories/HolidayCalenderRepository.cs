using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExcelDataReader;
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

        public async Task<List<HolidayCalendar>> HolidayCalendarList(string Location)
        {
            var holidaylist= await (from h in _context.HolidayCalendar
                                      where h.LOCATION.ToLower() == Location.ToLower()
                                      select new HolidayCalendar
                                      {
                                        HOLIDAYCALENDERID=h.HOLIDAYCALENDERID,
                                        HOLIDAY=h.HOLIDAY,
                                        DATE=h.DATE,
                                        DAY=h.DAY,
                                        REMARKS=h.REMARKS,
                                        LOCATION=h.LOCATION,
                                        YEAR=h.YEAR,
                                        ISACTIVE=h.ISACTIVE
                                      }).ToListAsync();
            return holidaylist;
        }

        public async Task<object> HolidayExcelUpload(IFormFile file)
        {
            try
            {
                List<string> messages = new List<string>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                if (file.Length > 0)
                {
                    Stream stream = new MemoryStream();
                    file.CopyTo(stream);
                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
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

                        if (finalRecords.Columns.Count != 8)
                        {
                            messages.Add("Invalid column length or column name");
                            return new
                            {
                                Status = 400,
                                Message = string.Join("\n", messages)
                            };
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
                            if (Convert.ToString(finalRecords.Rows[i]["Location"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Location"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["Year"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["Year"]) + " is required";
                            }
                            if (Convert.ToString(finalRecords.Rows[i]["IsActive"]).Equals(""))
                            {
                                return Convert.ToString(finalRecords.Columns["IsActive"]) + " is required";
                            }
                            // var bcnExist = await _context.HolidayCalendar.FirstOrDefaultAsync(x => x.Description.ToLower() == Convert.ToString(finalRecords.Rows[i]["Description"]).ToLower());
                            // bool IsbcnExist = bcnExist != null;
                            // if (IsbcnExist)
                            // {
                            //     messages.Add(Convert.ToString(finalRecords.Rows[i]["Description"]) + " " + "Already Exist");
                            //     //return string.Join("\n", messages);

                            // }
                            // else
                            // {

                                var holidaydata = new HolidayCalendar
                                {
                                    HOLIDAY=Convert.ToString(finalRecords.Rows[i]["Holiday"]),
                                    DATE=Convert.ToDateTime(finalRecords.Rows[i]["Date"]),
                                    DAY =Convert.ToString(finalRecords.Rows[i]["Day"]),
                                    REMARKS=Convert.ToString(finalRecords.Rows[i]["Remarks"]),
                                    LOCATION =Convert.ToString(finalRecords.Rows[i]["Locations"]),
                                    YEAR=Convert.ToString(finalRecords.Rows[i]["Year"]),
                                    ISACTIVE=Convert.ToBoolean(finalRecords.Rows[i]["IsActive"]),
                                    BATCHID= Guid.NewGuid().ToString()               
                                };
                                await _context.HolidayCalendar.AddAsync(holidaydata);
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
            var holidaylist= await (from h in _context.HolidayCalendar
                                      where h.LOCATION.ToLower() == Location.ToLower()
                                      select new LocationDto
                                      {
                                        HolidayCalendarId=h.HOLIDAYCALENDERID,
                                        Locations=h.LOCATION
                                      }).ToListAsync();
            return holidaylist;
        }
    }
}