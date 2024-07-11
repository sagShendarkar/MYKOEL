
using API.Entities;
using AutoMapper;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Master;
using MyKoel_Domain.Models.Masters;
namespace API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<MoodTodayDto, MoodToday>();
            CreateMap<MoodToday, MoodTodayDto>();
            CreateMap<WallpaperDto, Wallpaper>();
            CreateMap<Wallpaper, WallpaperDto>();
            CreateMap<UserAccessMappingDto, UserAccessMapping>();
            CreateMap<UserAccessMapping, UserAccessMappingDto>();
            CreateMap<UserProfileDto, AppUser>();
            CreateMap<AppUser, UserProfileDto>();

            CreateMap<SectionTrnDto, SectionTrnDto>();
            CreateMap<AddSectionTrnDto, SectionTransaction>();
            CreateMap<SectionTransaction, AddSectionTrnDto>();
            CreateMap<AddSectionTrnDto, Attachments>();
            CreateMap<Attachments, AddSectionTrnDto>();
            CreateMap<AttachmentDto, Attachments>();
            CreateMap<Attachments, AttachmentDto>();
            CreateMap<AddSectionTrnDto, AddSectionTrnDto>();

        }
    }
}