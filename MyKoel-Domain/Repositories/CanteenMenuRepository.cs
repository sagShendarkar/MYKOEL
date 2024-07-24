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

        public async Task<CanteenMenuListDto> CanteenMenuList(DateTime Date, string Location)
        {
            var canteenMenuList = await (from c in _context.CanteenMenus
                                         where c.DATE.Date == Date.Date &&   (!string.IsNullOrEmpty(Location) ? c.Location.ToLower() == Location.ToLower():true)
                                         group c by new { c.DATE,c.Location} into g
                                         select new CanteenMenuListDto
                                         {
                                             Date = g.Key.DATE,
                                             Location = g.Key.Location,
                                             BreakfastList = (from b in _context.BreakFasts
                                                              where g.Any(c => c.BREAKFASTID == b.BREAKFASTID)
                                                              select new BreakFastDto
                                                              {
                                                                  BreakFastId = b.BREAKFASTID,
                                                                  BreakFastName = b.BREAKFASTNAME,
                                                                  IsActive = b.ISACTIVE
                                                              }).ToList(),
                                             LunchList = (from l in _context.LunchMaster
                                                          where g.Any(c => c.LUNCHID == l.LUNCHID)
                                                          select new LunchDto
                                                          {
                                                              LunchId = l.LUNCHID,
                                                              LunchName = l.LUNCHNAME,
                                                              IsActive = l.ISACTIVE
                                                          }).ToList()
                                         })
                                         .FirstOrDefaultAsync();

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