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
    public class BirthdayMessageRepository:IBirthdayMessageRepository,IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BirthdayMessageRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        public void AddNewMessage(BirthdayMessage message)
        {
            _context.Entry(message).State = EntityState.Added;

        }

        public async Task<bool> MessageExists(int Day)
        {
            return await _context.BirthdayMessages.AnyAsync(x => x.DAY==Day);
        }

        public void DeleteMessage(BirthdayMessage message)
        {
            message.ISACTIVE = false;
            _context.Entry(message).State = EntityState.Modified;
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

        public async Task<BirthdayMessage> GetMessageById(int Id)
        {
            var message = await _context.BirthdayMessages
           .SingleOrDefaultAsync(x => x.ID == Id);
            return message;
        }

        public async Task<PagedList<BirthdayMessageDto>> BirthdayMessageList(ParameterParams parameterParams)
        {
           var message= (from b in _context.BirthdayMessages
                        where b.ISACTIVE==true
                        select new BirthdayMessageDto{
                          Id=b.ID,
                          Day=b.DAY,
                          Message=b.MESSAGE,
                          IsActive=b.ISACTIVE
                        }).OrderBy(c=>c.Day).AsQueryable();
         if(parameterParams.Day > 0){
            message= message.Where(s=>s.Day==parameterParams.Day);
         }
         if(parameterParams.searchPagination!=null){
               message=message.Where(x=> x.Message.ToLower().Contains(parameterParams.searchPagination.ToLower()));
         }  
          return await PagedList<BirthdayMessageDto>.CreateAsync(message.ProjectTo<BirthdayMessageDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(),parameterParams.PageNumber,parameterParams.PageSize);
   
        }

        public void UpdateMessage(BirthdayMessage message)
        {
            _context.Entry(message).State = EntityState.Modified;

        }

    }
}