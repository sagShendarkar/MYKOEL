using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Reflection.Emit;
using System.Security.Claims;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Entities;
using Microsoft.AspNetCore.Http;
using MyKoel_Domain.Interfaces;

namespace MyKoel_Domain.Data
{
    public class DataContext
    : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions options,IHttpContextAccessor httpContextAccessor,ICurrentUserService currentUserService,IDateTime dateTime)
  : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
            

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        }

         public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int userId = _currentUserService.getUserId();
           // int companyId = _currentUserService.getCompanyId();
            foreach (var entry in ChangeTracker.Entries<AuditableEntities>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedDate = _dateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedDate = _dateTime.UtcNow;
                        break;
                }
            }
            OnBeforeSaveChanges(userId);
            int result=0;
            try
            {
               result = await base.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex){
              
            };
            return result;
        }

        private void OnBeforeSaveChanges(int userId)
        {
            ChangeTracker.DetectChanges();
        }

   }
            
    }
