using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs.EnumDtos;
using MyKoel_Domain.Enums;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnumController : ControllerBase
    {
         private readonly DataContext _dataContext;

        public EnumController(DataContext dataContext){
            _dataContext=dataContext;
        }

        [HttpGet("CategoryDD")]
        public List<CategoryDto> CategoryDD()
        {
            var enums = ((Category[])Enum.GetValues(typeof(Category)))
            .Select(c => new CategoryDto()
            {
                Id = (int)c,
                Name = c.ToString()
            }).ToList();
            return enums; 
        }

        [HttpGet("TypeDD")]
        public List<TypeDto> TypeDD()
        {
            var enums = ((ValuesType[])Enum.GetValues(typeof(ValuesType)))
            .Select(c => new TypeDto()
            {
                Id = (int)c,
                TypeName = c.ToString()
            }).ToList();
            return enums; 
        }


        [HttpGet("JobStatusDD")]
        public List<CategoryDto> JobStatusDD()
        {
            var enums = ((JobStatus[])Enum.GetValues(typeof(JobStatus)))
            .Select(c => new CategoryDto()
            {
                Id = (int)c,
                Name = c.ToString()
            }).ToList();
            return enums; 
        }

        [HttpGet("JobTypeDD")]
        public List<CategoryDto> JobTypeDD()
        {
            var enums = ((JobType[])Enum.GetValues(typeof(JobType)))
            .Select(c => new CategoryDto()
            {
                Id = (int)c,
                Name = c.ToString()
            }).ToList();
            return enums; 
        }


    }
}