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
    public class SectionTransactionRepository: ISectionTrnRepository,IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public SectionTransactionRepository(DataContext context,IMapper mapper,IImageService imageService)
        {
            _context = context;
            _mapper=mapper;
            _imageService=imageService;
        }
         
        public async Task<SectionTransaction> GetSectionById(int Id) 
        {
             var SectionTransaction = await _context.SectionTransactions
            .SingleOrDefaultAsync(x=>x.SECTIONID==Id);
            return SectionTransaction;
        }
        

         public void UpdateSection(SectionTransaction SectionTransaction)
        {
            _context.Entry(SectionTransaction).State=EntityState.Modified;

        }

        public void DeleteSection(SectionTransaction SectionTransaction)
        {
             SectionTransaction.ISACTIVE=false;
             _context.Entry(SectionTransaction).State=EntityState.Modified;
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

        public void AddNewSection(SectionTransaction SectionTransaction)
        {
            _context.Entry(SectionTransaction).State=EntityState.Added;
        }

        public async Task<PagedList<AddSectionTrnDto>> GetSectionList(ParameterParams parameterParams)
        {
           var announcement=(from cn in _context.SectionTransactions
                            where cn.ISACTIVE==true
                            select new AddSectionTrnDto
                            {
                                SECTIONID=cn.SECTIONID,
                                TITLE=cn.TITLE,
                                DESCRIPTION=cn.DESCRIPTION,
                                STARTDATE=cn.STARTDATE,
                                ENDDATE=cn.ENDDATE,
                                ISACTIVE=cn.ISACTIVE,
                                ISIMAGE=cn.ISIMAGE,
                                FLAG=cn.FLAG,
                                SEQUENCE=cn.SEQUENCE,
                                CATEGORY=cn.CATEGORY,
                                IsHtml=cn.IsHtml,
                                Attachment = (from a in _context.Attachments
                                            where a.SECTIONID==cn.SECTIONID
                                 select new AttachmentDto
                                {
                                    ATTACHMENTID=a.ATTACHMENTID,
                                    SECTIONID=a.SECTIONID,
                                    ISPOPUP=a.ISPOPUP,
                                    ISREDIRECTED=a.ISREDIRECTED,
                                    FILENAME = a.FILENAME,
                                    FILETYPE = a.FILETYPE,
                                    TITLE=a.TITLE,
                                    ISACTIVE=a.ISACTIVE,
                                    IMAGE= !string.IsNullOrEmpty(a.PATH) ? _imageService.ConvertLocalImageToBase64(a.PATH) : null,
                                    PATH = a.PATH,
                                    IMAGEFLAG=a.IMAGEFLAG
                                }).ToList() 

                            }).OrderByDescending(c=>c.SECTIONID).AsQueryable();
             if(!string.IsNullOrEmpty(parameterParams.Flag)){
                announcement=announcement.Where(c=>c.FLAG==parameterParams.Flag);
             }
          return await PagedList<AddSectionTrnDto>.CreateAsync(announcement.ProjectTo<AddSectionTrnDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(),parameterParams.PageNumber,parameterParams.PageSize);
   
        }

        public async Task<Attachments> GetAttachmentById(int Id) 
        {
             var AttachmentTransaction = await _context.Attachments
            .SingleOrDefaultAsync(x=>x.ATTACHMENTID==Id);
            return AttachmentTransaction;
        }
        

         public void UpdateAttachment(Attachments attachment)
        {
            _context.Entry(attachment).State=EntityState.Modified;

        }
         public void AddAttachment(Attachments attachments)
        {
        
            _context.Entry(attachments).State=EntityState.Added;
        }

        public async Task<AddSectionTrnDto> GetSectionDetailsById(int Id)
        {
            var section=await (from cn in _context.SectionTransactions
                            where cn.ISACTIVE==true && cn.SECTIONID==Id  
                            select new AddSectionTrnDto
                            {
                                SECTIONID=cn.SECTIONID,
                                TITLE=cn.TITLE,
                                DESCRIPTION=cn.DESCRIPTION,
                                STARTDATE=cn.STARTDATE,
                                ENDDATE=cn.ENDDATE,
                                ISACTIVE=cn.ISACTIVE,
                                ISIMAGE=cn.ISIMAGE,
                                FLAG=cn.FLAG,
                                SEQUENCE=cn.SEQUENCE,
                                CATEGORY=cn.CATEGORY,
                                IsHtml=cn.ISACTIVE,
                                Attachment = (from a in _context.Attachments
                                            where a.SECTIONID==Id
                                 select new AttachmentDto
                                {
                                    ATTACHMENTID=a.ATTACHMENTID,
                                    SECTIONID=a.SECTIONID,
                                    ISPOPUP=a.ISPOPUP,
                                    ISREDIRECTED=a.ISREDIRECTED,
                                    FILENAME = a.FILENAME,
                                    FILETYPE = a.FILETYPE,
                                    TITLE=a.TITLE,
                                    ISACTIVE=a.ISACTIVE,
                                    IMAGE= !string.IsNullOrEmpty(a.PATH) ? _imageService.ConvertLocalImageToBase64(a.PATH) : null,
                                    PATH = a.PATH,
                                    IMAGEFLAG=a.IMAGEFLAG
                                }).ToList() 

                            }).OrderByDescending(c=>c.SECTIONID).FirstOrDefaultAsync();
           return section;
        }
    }
}
