using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Repositories
{
    public class MoodTodayRepository : IMoodTodayRepository
    { 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MoodTodayRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper; 
        }

        public void AddMoodToday(MoodToday moodtoday)
        {
            _context.Entry(moodtoday).State = EntityState.Added;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}