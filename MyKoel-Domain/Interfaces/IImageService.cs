using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.Interfaces
{
    public interface IImageService
    {
            string ConvertLocalImageToBase64(string filePath);
            string ConvertFileToBase64(string filePath);

    }
}