using System.ComponentModel.DataAnnotations;

namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }

    }

}
