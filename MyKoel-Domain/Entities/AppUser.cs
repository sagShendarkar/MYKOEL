
using Microsoft.AspNetCore.Identity;
using MyKoel_Domain.Models.Master;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        #nullable disable
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<UserAccessMapping> userMenuMaps { get; set; }
        public int EMPID { get; set; }
        public string EmpName { get; set; }
        public string TicketNo { get; set; }
        public string Grade { get; set; }
        public string SBUNo { get; set; }
        public string CostCode { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int MANAGERID { get; set; }
        public string AppName { get; set; }
        public string ManagerEmailID { get; set; }
        public string ManagerTicketNo { get; set; }
        public DateTime DOB {get; set; }
        public string? ProfileImage {get;set;}
    }
}