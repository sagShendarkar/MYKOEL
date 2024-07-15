using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using iot_Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Repositories
{
    public class LunchRepository: ILunchRepository, IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LunchRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        public void AddNewLunch(LunchMaster lunch)
        {
            _context.Entry(lunch).State = EntityState.Added;

        }

        public async Task<bool> LunchExists(string Name)
        {
            return await _context.LunchMaster.AnyAsync(x => x.LUNCHNAME.ToLower() == Name.ToLower());
        }

        public void DeleteLunch(LunchMaster lunch)
        {
            lunch.ISACTIVE = false;
            _context.Entry(lunch).State = EntityState.Modified;
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

        public async Task<LunchMaster> GetLunchById(int Id)
        {
            var Lunch = await _context.LunchMaster
           .SingleOrDefaultAsync(x => x.LUNCHID == Id);
            return Lunch;
        }

        public async Task<PagedList<LunchDto>> GetLunchList(ParameterParams parameterParams)
        {
           var Lunch=_context.LunchMaster.Where(x=>x.ISACTIVE==true)
                            .OrderByDescending(c=>c.LUNCHID).AsQueryable();

         if(parameterParams.searchPagination!=null){
               Lunch=Lunch.Where(x=> x.LUNCHNAME.ToLower().Contains(parameterParams.searchPagination.ToLower()));
         }  
          return await PagedList<LunchDto>.CreateAsync(Lunch.ProjectTo<LunchDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(),parameterParams.PageNumber,parameterParams.PageSize);
   
        }

        public  async Task<List<LunchDto>> GetDropdownList(int LunchId, string? Desc)
        {
            var Lunchdata= await (from b in _context.LunchMaster
                            where b.ISACTIVE==true
                            select new LunchDto{
                                LunchId = b.LUNCHID,
                                LunchName=b.LUNCHNAME,
                               IsActive=b.ISACTIVE
                            }).ToListAsync();
                      if(!string.IsNullOrEmpty(Desc))
                      {
                         Lunchdata=Lunchdata.Where(x=> x.LunchName.ToLower().Contains(Desc.ToLower())).ToList();
                      }
              
            return Lunchdata;
        }

        public void UpdateLunch(LunchMaster lunch)
        {
            _context.Entry(lunch).State = EntityState.Modified;

        }

    }
}