using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Interfaces
{
    public interface IUserRepository
    {
          void UpdateUser(AppUser user);
          Task<bool> SaveAllAsync();
          Task<AppUser> GetUserById(int Id);
          void AddNewUser(AppUser user);
          void AddUserMenuAccess(UserAccessMapping userAccess);
          Task<List<UserDropdown>> GetUserDropdown(string UserName);

    }
}