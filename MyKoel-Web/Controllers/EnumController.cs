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

    }
}