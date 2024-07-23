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
    public class BreakFastController : ControllerBase
    {
        private readonly IBreakFastRepository _breakFastRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BreakFastController(IBreakFastRepository breakFastRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _breakFastRepository = breakFastRepository;
        }

        [HttpGet("ShowBreakFastList")]
        public async Task<List<BreakFastDto>> ShowBreakFastList([FromQuery] ParameterParams parameterParams)
        {
            var breakfast = await _breakFastRepository.GetBreakfastList(parameterParams);
            Response.AddPaginationHeader(breakfast.CurrentPage, breakfast.PageSize,
                    breakfast.TotalCount, breakfast.TotalPages);

            return breakfast;
        }
        [HttpPost("AddBreakfast")]
        public async Task<object> AddNewBreakfast(BreakFastDto breakfastDto)
        {
            try
            {
                if (await _breakFastRepository.BreakfastExists(breakfastDto.BreakFastName))
                    return BadRequest("Breakfast name is already exists");
                var breakfast = _mapper.Map<BreakFast>(breakfastDto);
                _breakFastRepository.AddNewBreakfast(breakfast);
                if (await _breakFastRepository.SaveAllAsync())
                {

                    return new 
                    {
                        Status = 200,
                        Message = "Data Added Successfully"
                    };
                }
                return  new 
                {
                    Status = 400,
                     Message = "Failed To Add Data"
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

        [HttpGet("ShowBreakfastById/{Id}")]
        public async Task<ActionResult<BreakFast>> GetBreakfastDetailsByCode(int Id)
        {
            var data = await _breakFastRepository.GetBreakfastById(Id);
            return data;
        }

        [HttpPost("UpdateBreakfastDetails")]
        public async Task<object> UpdateBreakfastDetails(BreakFastDto updateBreakfastDto)
        {
            try
            {
                var breakfast = await _breakFastRepository.GetBreakfastById(updateBreakfastDto.BreakFastId);
                _mapper.Map(updateBreakfastDto, breakfast);
                _breakFastRepository.UpdateBreakFast(breakfast);
                if (await _breakFastRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Updated SuccessFully"
                    };
                    return Ok(result);
                }
                return  new 
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

        [HttpPost("DeleteBreakfast/{Id}")]
        public async Task<object> DeleteBreakfast(int Id)
        {

            var data = await _breakFastRepository.GetBreakfastById(Id);
            try
            {
                _breakFastRepository.DeleteBreakfast(data);
                if (await _breakFastRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Deleted Successfully"
                    };
                    return Ok(result);
                }
                return  new 
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
        [HttpGet("BreakfastDropdownList")]
        public async Task<List<BreakFastDto>> BreakfastDropdownList(int BreakfastId, string? Desc)
        {
            return await _breakFastRepository.GetDropdownList(BreakfastId, Desc);
        }


    }
}