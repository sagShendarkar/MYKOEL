using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Interfaces
{
    public interface IMoodTodayRepository
    {
        void AddMoodToday(MoodToday moodtoday);
        Task<bool> SaveAllAsync();
    }
}