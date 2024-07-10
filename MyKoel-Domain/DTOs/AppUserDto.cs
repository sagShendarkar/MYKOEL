
namespace MyKoel_Domain.DTOs
{
    #nullable disable
    public class AppUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    } 

    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string ProfileImage {get;set;}
        public string ProfileSrc{ get; set;}  
    } 

    public class UserDropdown
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmpName { get; set; }
        public string TicketNo { get; set; }

    }
   
}