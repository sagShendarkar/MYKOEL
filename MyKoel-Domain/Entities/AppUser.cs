
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

    }
}