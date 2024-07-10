
using API.Entities;
using AutoMapper;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Master;
namespace API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        { 
            CreateMap<MoodTodayDto, MoodToday>();
            CreateMap<MoodToday, MoodTodayDto>();
        }
    }
}