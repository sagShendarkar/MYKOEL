using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Master;

namespace MyKoel_Domain.Repositories
{
    public class UserRepository:IUserRepository,IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
    
        public async Task<AppUser> GetUserById(int Id) 
        {
             var User = await _context.Users.SingleOrDefaultAsync(x=>x.Id==Id);
            return User;
        }
        

         public void UpdateUser(AppUser User)
        {
            _context.Entry(User).State=EntityState.Modified;

        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
       public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(isDisposed) return;
            if (disposing)
            {
                // free managed resources
            }
            isDisposed = true;
            // free native resources here if there are any
        }

        public void AddNewUser(AppUser User)
        {
            _context.Entry(User).State=EntityState.Added;
        }

        public void AddUserMenuAccess(UserAccessMapping userAccess)
        {
            _context.Entry(userAccess).State=EntityState.Added;
        }

        public async Task<List<UserDropdown>> GetUserDropdown(string UserName)
        {
            var userData=  await (from u in _context.Users
                          select new UserDropdown
                          {
                            UserId=u.Id,
                            UserName=u.UserName,
                            Email=u.Email,
                            EmpName=u.EmpName,
                             TicketNo=u.TicketNo    
                          }).ToListAsync();
            if(!string.IsNullOrEmpty(UserName))
            {
                userData=userData.Where(x => x.UserName.ToLower().Equals(UserName.ToLower())).ToList();
            }
            return userData;

        }
    }
}