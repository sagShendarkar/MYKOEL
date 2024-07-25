using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanteenMenusController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICanteenMenuRepository _canteenMenuRepository;
        private readonly IMapper _mapper;

        public CanteenMenusController(DataContext context, IMapper mapper, ICanteenMenuRepository canteenMenuRepository)
        {
            _context = context;
            _mapper = mapper;
            _canteenMenuRepository = canteenMenuRepository;
        }

        [HttpPost("AddCanteenMenus")]
        public async Task<object> AddCanteenMenus(CanteenDto canteenDto)
        {
            try
            {
                if (canteenDto != null)

                    await _canteenMenuRepository.RemoveCanteenMenu(canteenDto.Date,canteenDto.Location);
                    foreach (var lunchId in canteenDto.LunchId)
                    {
                        var canteenMenu = new CanteenMenus
                        {
                            DATE = canteenDto.Date,
                            LUNCHID = lunchId,
                            Location = canteenDto.Location,
                            BREAKFASTID=null
                        };

                        _context.CanteenMenus.Add(canteenMenu);
                    }

                    foreach (var breakfastId in canteenDto.BreakFastId)
                    {
                        var canteenMenu = new CanteenMenus
                        {
                            DATE = canteenDto.Date,
                            BREAKFASTID = breakfastId,
                            Location = canteenDto.Location,
                            LUNCHID=null
                        };

                        _context.CanteenMenus.Add(canteenMenu);
                    }

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return new
                        {
                            Status = 200,
                            Message = "Menu Added Successfully"
                        };
                    }
                    else
                    {
                        return new
                        {
                            Status = 400,
                            Message = "Failed To Add Data"
                        };
                    }
            }
            catch (Exception ex)
            {
                return new
                {
                    Message = ex.Message
                };
            }
        }

        [HttpGet("BreakfastMenuList")]
        public async Task<List<CanteenMenuListDto>> BreakfastList(DateTime Date, string Location)
        {
            return await _canteenMenuRepository.BreakfastList(Date, Location);
        }

        [HttpGet("LunchMenuList")]
        public async Task<List<CanteenMenuListDto>> LunchMenuList(DateTime Date, string Location)
        {
            return await _canteenMenuRepository.LunchList(Date, Location);
        }

        [HttpGet("CanteenMenuList")]
        public async Task<CanteenMenusDto> CanteenMenuList(DateTime Date, string Location)
        {
            return await _canteenMenuRepository.CanteenMenusList(Date, Location);
        }
    }
}
