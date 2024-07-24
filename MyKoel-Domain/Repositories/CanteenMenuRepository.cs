using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Repositories
{
    public class CanteenMenuRepository : ICanteenMenuRepository, IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CanteenMenuRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void AddCanteenMenus(CanteenMenus canteen)
        {
            _context.Entry(canteen).State = EntityState.Added;
        }

        public async Task<List<CanteenMenuListDto>> BreakfastList(DateTime Date, string Location)
        {
            var canteenMenuList = await (from c in _context.CanteenMenus
                                         join b in _context.BreakFasts
                                         on c.BREAKFASTID equals b.BREAKFASTID
                                         where c.DATE.Date == Date.Date && (!string.IsNullOrEmpty(Location) ? c.Location.ToLower() == Location.ToLower():true)
                                         select new CanteenMenuListDto
                                         {
                                             Date = c.DATE,
                                             Location = c.Location,
                                             Name=b.BREAKFASTNAME
                                         }).ToListAsync();
            return canteenMenuList;
        }
        public async Task<List<CanteenMenuListDto>> LunchList(DateTime Date, string Location)
        {
            var canteenMenuList = await (from c in _context.CanteenMenus
                                         join b in _context.LunchMaster
                                         on c.LUNCHID equals b.LUNCHID
                                         where c.DATE.Date == Date.Date && (!string.IsNullOrEmpty(Location) ? c.Location.ToLower() == Location.ToLower():true)
                                         select new CanteenMenuListDto
                                         {
                                             Date = c.DATE,
                                             Location = c.Location,
                                             Name=b.LUNCHNAME
                                         }).ToListAsync();
            return canteenMenuList;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {

            }
            isDisposed = true;
        }

    }
}