using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKoel_Domain.Interfaces;

namespace MyKoel_Domain.Repositories
{
    public class ImageService : IImageService
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
                case ".Pdf":
                    return "Pdf";
                default:
                    throw new ArgumentException($"Unsupported image format: {extension}");
            }
        }


        private string GetFileMimeType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".svg":
                    return "image/svg+xml";
                case ".pdf":
                    return "application/pdf";
                case ".txt":
                    return "text/plain";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                default:
                    throw new ArgumentException($"Unsupported file format: {extension}");
            }
        }

        public string ConvertFileToBase64(string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(fileBytes);
                string extension = Path.GetExtension(filePath);
                string mimeType = GetFileMimeType(extension);

                string base64FileSource = $"data:{mimeType};base64,{base64String}";

                return base64FileSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting file to base64: {ex.Message}");
                return null;
            }
        }


    }
}