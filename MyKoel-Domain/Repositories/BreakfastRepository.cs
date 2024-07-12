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
    public class BreakfastRepository : IBreakFastRepository,IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BreakfastRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        public void AddNewBreakfast(BreakFast breakfast)
        {
            _context.Entry(breakfast).State = EntityState.Added;

        }

        public async Task<bool> BreakfastExists(string Name)
        {
            return await _context.BreakFasts.AnyAsync(x => x.BreakFastName.ToLower() == Name.ToLower());
        }

        public void DeleteBreakfast(BreakFast breakfast)
        {
            breakfast.IsActive = false;
            _context.Entry(breakfast).State = EntityState.Modified;
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

        public async Task<BreakFast> GetBreakfastById(int Id)
        {
            var breakfast = await _context.BreakFasts
           .SingleOrDefaultAsync(x => x.BreakFastId == Id);
            return breakfast;
        }

        public async Task<PagedList<BreakFastDto>> GetBreakfastList(ParameterParams parameterParams)
        {
           var breakfast=_context.BreakFasts.Where(x=>x.IsActive==true)
                            .OrderByDescending(c=>c.BreakFastId).AsQueryable();

         if(parameterParams.searchPagination!=null){
               breakfast=breakfast.Where(x=> x.BreakFastName.ToLower().Contains(parameterParams.searchPagination.ToLower()));
         }  
          return await PagedList<BreakFastDto>.CreateAsync(breakfast.ProjectTo<BreakFastDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(),parameterParams.PageNumber,parameterParams.PageSize);
   
        }

        public  async Task<List<BreakFastDto>> GetDropdownList(int BreakFastId, string? Desc)
        {
            var breakfastdata= await (from b in _context.BreakFasts
                            where b.IsActive==true
                            select new BreakFastDto{
                                BreakFastId = b.BreakFastId,
                                BreakFastName=b.BreakFastName,
                               IsActive=b.IsActive
                            }).ToListAsync();
                      if(!string.IsNullOrEmpty(Desc))
                      {
                         breakfastdata=breakfastdata.Where(x=> x.BreakFastName.ToLower().Contains(Desc.ToLower())).ToList();
                      }
              
            return breakfastdata;
        }

        public void UpdateBreakFast(BreakFast breakfast)
        {
            _context.Entry(breakfast).State = EntityState.Modified;

        }
    }
}