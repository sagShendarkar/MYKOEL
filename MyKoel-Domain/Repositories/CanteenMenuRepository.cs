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

        public async Task<CanteenMenuListDto> CanteenMenuList(DateTime Date)
        {
            var canteenMenuList = await (from c in _context.CanteenMenus
                                         where c.Date.Date == Date.Date
                                         group c by c.Date into g
                                         select new CanteenMenuListDto
                                         {
                                             Date = g.Key.Date,
                                             BreakfastList = (from b in _context.BreakFasts
                                                              where g.Any(c => c.BreakFastId == b.BreakFastId)
                                                              select new BreakFastDto
                                                              {
                                                                  BreakFastId = b.BreakFastId,
                                                                  BreakFastName = b.BreakFastName,
                                                                  IsActive = b.IsActive
                                                              }).ToList(),
                                             LunchList = (from l in _context.LunchMaster
                                                          where g.Any(c => c.LunchId == l.LunchId)
                                                          select new LunchDto
                                                          {
                                                              LunchId = l.LunchId,
                                                              LunchName = l.LunchName,
                                                              IsActive = l.IsActive
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