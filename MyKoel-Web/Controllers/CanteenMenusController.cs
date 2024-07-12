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
        public async Task<object> AddCanteenMenus(List<CanteenDto> canteenDto)
        {
            try
            {
                if (canteenDto != null)
                {
                    foreach (var canteenMenu in canteenDto)
                    {
                        var canteenmenu = _mapper.Map<CanteenMenus>(canteenMenu);
                        _canteenMenuRepository.AddCanteenMenus(canteenmenu);

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
                else
                {
                    return new
                    {
                        Status = 400,
                        Message = "Invalid Data"
                    };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("CanteenMenuList")]
        public async Task<CanteenMenuListDto> CanteenMenuList(DateTime Date)
        {
            return await _canteenMenuRepository.CanteenMenuList(Date);
        }


    }
}
