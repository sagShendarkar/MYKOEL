using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyKoel_Domain.DTOs
{
    public class UploadExcelDto
    {
        public IFormFile Excelfile { get; set; }
        public string Location { get; set; }
    }
}