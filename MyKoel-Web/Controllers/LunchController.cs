using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
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
    public class LunchController : ControllerBase
    {
        private readonly ILunchRepository _lunchRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LunchController(ILunchRepository lunchRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _lunchRepository = lunchRepository;
        }

        [HttpGet("ShowLunchList")]
        public async Task<List<LunchDto>> ShowLunchList([FromQuery] ParameterParams parameterParams)
        {
            var Lunch = await _lunchRepository.GetLunchList(parameterParams);
            Response.AddPaginationHeader(Lunch.CurrentPage, Lunch.PageSize,
                    Lunch.TotalCount, Lunch.TotalPages);

            return Lunch;
        }
        [HttpPost("AddLunch")]
        public async Task<object> AddNewLunch(LunchDto LunchDto)
        {
            try
            {
                if (await _lunchRepository.LunchExists(LunchDto.LunchName))
                    return BadRequest("Lunch name is already exists");
                var Lunch = _mapper.Map<LunchMaster>(LunchDto);
                _lunchRepository.AddNewLunch(Lunch);
                if (await _lunchRepository.SaveAllAsync())
                {

                    return new
                    {
                        Status = 200,
                        Message = "Data Added Successfully"
                    };
                }
                return new
                {
                    Status = 400,
                    Message = "Failed To Add Data"
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("ShowLunchById/{Id}")]
        public async Task<object> GetLunchDetailsByCode(int Id)
        {
            var data = await _lunchRepository.GetLunchById(Id);
            return data;
        }

        [HttpPost("UpdateLunchDetails")]
        public async Task<object> UpdateLunchDetails(LunchDto updateLunchDto)
        {
            try
            {
                var Lunch = await _lunchRepository.GetLunchById(updateLunchDto.LunchId);
                _mapper.Map(updateLunchDto, Lunch);
                _lunchRepository.UpdateLunch(Lunch);
                if (await _lunchRepository.SaveAllAsync())
                {

                    var result = new
                    {
                        Status = 200,
                        Message = "Data Updated SuccessFully"
                    };
                    return Ok(result);
                }
                return new
                {
                    Status = 400,
                    Message = "Failed To Update Data"
                };
            }
            catch (Exception ex)
            {
                var result = new
                {
                    Status = 400,
                    Message = ex.Message
                };
                return Ok(result);

            }
        }

        [HttpPost("DeleteLunch/{Id}")]
        public async Task<object> DeleteLunch(int Id)
        {

            var data = await _lunchRepository.GetLunchById(Id);
            try
            {
                _lunchRepository.DeleteLunch(data);
                if (await _lunchRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Deleted Successfully"
                    };
                    return Ok(result);
                }
                return new
                {
                    Status = 400,
                    Message = "Failed To Delete Data"
                };

            }
            catch (Exception ex)
            {
                var result = new
                {
                    Status = 400,
                    Message = ex.Message
                };
                return BadRequest(result);

            }

        }
        [HttpGet("LunchDropdownList")]
        public async Task<List<LunchDto>> LunchDropdownList(int LunchId, string? Desc)
        {
            return await _lunchRepository.GetDropdownList(LunchId, Desc);
        }


    }
}
