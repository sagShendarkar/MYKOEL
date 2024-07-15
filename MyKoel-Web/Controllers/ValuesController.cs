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
    public class ValuesController : ControllerBase
    {
        private readonly IValuesRepository _valuesRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ValuesController(IValuesRepository valuesRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _valuesRepository = valuesRepository;
        }

        [HttpGet("ShowValuesList")]
        public async Task<List<ValuesDto>> ShowValuesList([FromQuery] ParameterParams parameterParams)
        {
            var values = await _valuesRepository.GetValuesList(parameterParams);
            Response.AddPaginationHeader(values.CurrentPage, values.PageSize,
                    values.TotalCount, values.TotalPages);

            return values;
        }
        [HttpPost("AddValues")]
        public async Task<object> AddNewValues(ValuesDto ValuesDto)
        {
            try
            {

                var values = _mapper.Map<ValuesMaster>(ValuesDto);
                _valuesRepository.AddNewValues(values);
                if (await _valuesRepository.SaveAllAsync())
                {
                    return new
                    {
                        Status = 200,
                        Message = "Data Added Successfully",
                    };
                }
                return new
                {
                    Status = 400,
                    Message = "Failed To Add Data",
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("ShowValuesById/{Id}")]
        public async Task<ActionResult<ValuesMaster>> GetvaluesDetailsByCode(int Id)
        {
            var data = await _valuesRepository.GetValuesById(Id);
            return data;
        }

        [HttpPost("UpdateValuesDetails")]
        public async Task<ActionResult> UpdateValuesDetails(ValuesDto updateValuesDto)
        {
            try
            {
                var values = await _valuesRepository.GetValuesById(updateValuesDto.VALUEID);
                _mapper.Map(updateValuesDto, values);
                _valuesRepository.UpdateValues(values);
                if (await _valuesRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Updated SuccessFully"
                    };
                    return Ok(result);
                }
                return BadRequest("Failed To Update Data");
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

        [HttpPost("DeleteValues/{Id}")]
        public async Task<ActionResult> Deletevalues(int Id)
        {

            var data = await _valuesRepository.GetValuesById(Id);
            try
            {
                _valuesRepository.DeleteValues(data);
                if (await _valuesRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Deleted Successfully"
                    };
                    return Ok(result);
                }
                return BadRequest("Failed To Delete Data");

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
        [HttpGet("ValuesDetailsById/{Id}")]
        public async Task<ActionResult<ValuesDto>> ValuesDetailsById(int Id)
        {
            var data = await _valuesRepository.GetValuesDetailById(Id);
            return data;
        }


    }
}