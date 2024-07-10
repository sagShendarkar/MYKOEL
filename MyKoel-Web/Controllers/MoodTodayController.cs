using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Data;
using AutoMapper;
using MyKoel_Domain.Models.Master;
namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodTodayController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMoodTodayRepository _moodtoday;
        private readonly IMapper _mapper;

        public MoodTodayController(DataContext context, IMapper mapper, IMoodTodayRepository moodtoday)
        {
            _context = context; _mapper = mapper;
            _moodtoday = moodtoday;
        }

        [HttpPost("AddMoodToday")]
        public async Task<ActionResult<MoodTodayDto>> AddMoodToday(MoodTodayDto moodmasterdto)
        {
            try
            { 
                moodmasterdto.ReportedDateTime = DateTime.Now;
                var mooddata = _mapper.Map<MoodToday>(moodmasterdto);
                _moodtoday.AddMoodToday(mooddata);
                if (await _moodtoday.SaveAllAsync())
                    return new MoodTodayDto
                    {
                        MoodId = moodmasterdto.MoodId,
                        Rating = moodmasterdto.Rating,
                    };
                return BadRequest("Failed To Add Data");

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}