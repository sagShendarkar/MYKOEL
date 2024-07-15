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
    public class ValuesRepository : IValuesRepository, IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ValuesRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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



        public void UpdateValues(ValuesMaster values)
        {
            _context.Entry(values).State = EntityState.Modified;

        }

        public void DeleteValues(ValuesMaster values)
        {
            values.ISACTIVE = false;
            _context.Entry(values).State = EntityState.Modified;
        }

        public async Task<ValuesMaster> GetValuesById(int Id)
        {
            var values = await _context.ValuesMaster
          .SingleOrDefaultAsync(x => x.VALUEID == Id);
            return values;
        }

        public void AddNewValues(ValuesMaster values)
        {
            _context.Entry(values).State = EntityState.Added;

        }

        public async Task<PagedList<ValuesDto>> GetValuesList(ParameterParams parameterParams)
        {
            var values =(from v in  _context.ValuesMaster
                        where v.ISACTIVE==true
                        select new ValuesDto {
                           VALUEID=v.VALUEID,
                           DESCRIPTION=v.DESCRIPTION,
                           TYPE=v.TYPE,
                           TYPENAME=v.TYPE.ToString(),
                           ISACTIVE=v.ISACTIVE
                        }) .OrderByDescending(c => c.VALUEID).AsQueryable();

            if (parameterParams.searchPagination != null)
            {
                values = values.Where(x => x.DESCRIPTION.ToLower().Contains(parameterParams.searchPagination.ToLower()));
            }
            return await PagedList<ValuesDto>.CreateAsync(values.ProjectTo<ValuesDto>(_mapper.ConfigurationProvider)
                                   .AsNoTracking(), parameterParams.PageNumber, parameterParams.PageSize);
        }

        public async Task<ValuesDto> GetValuesDetailById(int Id)
        {
            var values = await (from v in  _context.ValuesMaster
                        where v.ISACTIVE==true
                        select new ValuesDto {
                           VALUEID=v.VALUEID,
                           DESCRIPTION=v.DESCRIPTION,
                           TYPE=v.TYPE,
                           TYPENAME=v.TYPE.ToString(),
                           ISACTIVE=v.ISACTIVE
                        }) .OrderByDescending(c => c.VALUEID).FirstOrDefaultAsync();
            return values;
        }
    }
}