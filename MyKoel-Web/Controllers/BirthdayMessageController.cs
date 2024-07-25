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
    public class BirthdayMessageController : ControllerBase
    {
         private readonly IBirthdayMessageRepository _birthdayMessageRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BirthdayMessageController(IBirthdayMessageRepository breakFastRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _birthdayMessageRepository = breakFastRepository;
        }

        [HttpGet("ShowBirthdayMessageList")]
        public async Task<List<BirthdayMessageDto>> ShowBirthdayMessageList([FromQuery] ParameterParams parameterParams)
        {
            var message = await _birthdayMessageRepository.BirthdayMessageList(parameterParams);
            Response.AddPaginationHeader(message.CurrentPage, message.PageSize,
                    message.TotalCount, message.TotalPages);

            return message;
        }
        [HttpPost("AddBirthdayMessage")]
        public async Task<object> AddBirthdayMessage(BirthdayMessageDto birthdayMessage)
        {
            try
            {
                if (await _birthdayMessageRepository.MessageExists(birthdayMessage.Day))
                    return BadRequest("Birthday Message is already exists");
                var message = _mapper.Map<BirthdayMessage>(birthdayMessage);
                _birthdayMessageRepository.AddNewMessage(message);
                if (await _birthdayMessageRepository.SaveAllAsync())
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

        [HttpGet("ShowMessageById/{Id}")]
        public async Task<ActionResult<BirthdayMessage>> GetMessageDetailsById(int Id)
        {
            var data = await _birthdayMessageRepository.GetMessageById(Id);
            return data;
        }

        [HttpPost("UpdateBirthdayMessage")]
        public async Task<object> UpdateBirthdayMessage(BirthdayMessageDto updateBreakfastDto)
        {
            try
            {
                var message = await _birthdayMessageRepository.GetMessageById(updateBreakfastDto.Id);
                _mapper.Map(updateBreakfastDto, message);
                _birthdayMessageRepository.UpdateMessage(message);
                if (await _birthdayMessageRepository.SaveAllAsync())
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

        [HttpPost("DeleteMessage/{Id}")]
        public async Task<object> DeleteMessage(int Id)
        {

            var data = await _birthdayMessageRepository.GetMessageById(Id);
            try
            {
                _birthdayMessageRepository.DeleteMessage(data);
                if (await _birthdayMessageRepository.SaveAllAsync())
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

    }
}