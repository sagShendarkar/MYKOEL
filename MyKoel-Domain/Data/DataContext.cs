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
using MyKoel_Domain.Models.Master;
using MyKoel_Domain.Models.Masters;

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
             public DbSet<MenuGroup> MenuGroups { get; set; }
             public DbSet<MainMenuGroup> MainMenuGroups { get; set; }
             public DbSet<Menus>Menus { get; set; }
             public DbSet<UserAccessMapping> UserMenuMap { get; set; }
             public DbSet<QuickLinks> QuickLinks { get; set; }
             public DbSet<Wallpaper> wallpaper { get; set; }
             public DbSet<SectionTransaction> SectionTransactions { get; set; }
             public DbSet<Attachments> Attachments { get; set; }
        public DbSet<MoodToday> MoodToday { get; set; }
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

           builder.Entity<MenuGroup>()
            .HasKey(m => m.MenuGroupId);

            builder.Entity<MenuGroup>()
                .HasOne<MainMenuGroup>(mg => mg.MainMenuGroup)
                .WithMany(m => m.MenuGroups)
                .HasForeignKey(st => st.MainMenuGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MainMenuGroup>()
                .HasKey(m => m.MainMenuGroupId);

                builder.Entity<Menus>()
            .HasKey(m => m.MenuId);

              builder.Entity<Menus>()
                .HasOne<MenuGroup>(mg => mg.MenuGroup)
                .WithMany(m => m.Menus)
                .HasForeignKey(st => st.MenuGroupId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserAccessMapping>()
                 .HasKey(m => m.AccessMappingId);

            builder.Entity<UserAccessMapping>()
                 .HasOne<Menus>(m => m.Menu)
                 .WithMany(m => m.userMenuMaps)
                 .HasForeignKey(st => st.MenuId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserAccessMapping>()
                 .HasOne<AppUser>(a => a.User)
                 .WithMany(m => m.userMenuMaps)
                 .HasForeignKey(st => st.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
                 
           builder.Entity<QuickLinks>()
            .HasKey(m => m.QuickLinkId);
            
           builder.Entity<Wallpaper>()
            .HasKey(m => m.WallpaperId);
            builder.Entity<MoodToday>()
         .HasKey(m => m.MoodId);

            builder.Entity<SectionTransaction>()
            .HasKey(m => m.SECTIONID);
            
                builder.Entity<Attachments>()
            .HasKey(m => m.ATTACHMENTID);

              builder.Entity<Attachments>()
                .HasOne<SectionTransaction>(mg => mg.SectionTransaction)
                .WithMany(m => m.Attachments)
                .HasForeignKey(st => st.SECTIONID)
                .OnDelete(DeleteBehavior.Restrict);


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
                        entry.Entity.CREATEDBY = userId;
                        entry.Entity.CREATEDDATE = _dateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UPDATEDBY = userId;
                        entry.Entity.UPDATEDDATE = _dateTime.UtcNow;
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
