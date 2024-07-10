using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Interfaces;

namespace MyKoel_Domain.Repositories
{
    public class ImageService: IImageService
{
    public string ConvertLocalImageToBase64(string filePath)
    {
        try
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);
            string extension = Path.GetExtension(filePath)?.ToLower();
            string format = GetImageFormat(extension);

            string base64ImageSource;
            if (format == "svg")
            {
                base64ImageSource = $"data:image/svg+xml;base64,{base64String}";
            }
            else
            {
                base64ImageSource = $"data:image/{format};base64,{base64String}";
            }

            return base64ImageSource;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error converting image to base64: {ex.Message}");
            return null;
        }
    }

    private string GetImageFormat(string extension)
    {
        switch (extension)
        {
            case ".jpg":
            case ".jpeg":
                return "jpeg";
            case ".png":
                return "png";
            case ".gif":
                return "gif";
            case ".bmp":
                return "bmp";
            case ".svg":
                return "svg"; 
            default:
                throw new ArgumentException($"Unsupported image format: {extension}");
        }
    }
}
}