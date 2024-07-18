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
    public class VacancyPostingRepository : IVacancyPosting, IDisposable
    {

        private bool isDisposed;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public VacancyPostingRepository(DataContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
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

        public async Task<PagedList<VacancyPostingDto>> GetVacancyList(ParameterParams parameterParams)
        {
            var vacancyData = (from v in _context.VacancyPosting
                               select new VacancyPostingDto
                               {
                                   VACANCYID = v.VACANCYID,
                                   GRADE = v.GRADE,
                                   JOBTITLE = v.JOBTITLE,
                                   DEPARTMENT = v.DEPARTMENT,
                                   LOCATION = v.LOCATION,
                                   JOBTYPE = v.JOBTYPE,
                                   JOBDESC = !string.IsNullOrEmpty(v.JOBDESC) ? _imageService.ConvertPdfToBase64(v.JOBDESC) : null,
                                   SALARYRANGE = v.SALARYRANGE,
                                   REQUIRMENTS = v.REQUIRMENTS,
                                   POSTEDDATE = v.POSTEDDATE,
                                   CLOSINGDATE = v.CLOSINGDATE,
                                   CONTACTINFO = v.CONTACTINFO,
                                   STATUS = v.STATUS,
                                   ISACTIVE = v.ISACTIVE,
                                   VACANCYCOUNT = v.VACANCYCOUNT
                               }).OrderByDescending(s => s.VACANCYID).AsQueryable();
            if (!string.IsNullOrEmpty(parameterParams.Flag))
            {
                vacancyData = vacancyData.Where(c => parameterParams.searchPagination.Contains(c.JOBTITLE)
                || parameterParams.searchPagination.Contains(c.JOBDESC) || parameterParams.searchPagination.Contains(c.LOCATION)
                );
            }
            return await PagedList<VacancyPostingDto>.CreateAsync(vacancyData.ProjectTo<VacancyPostingDto>(_mapper.ConfigurationProvider)
                                 .AsNoTracking(), parameterParams.PageNumber, parameterParams.PageSize);

        }

        public void UpdateVacancy(VacancyPosting vacancy)
        {
            _context.Entry(vacancy).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void DeleteVacancy(VacancyPosting vacancy)
        {
            vacancy.ISACTIVE = false;
            _context.Entry(vacancy).State = EntityState.Modified;

        }

        public async Task<VacancyPosting> GetVacancyById(int Id)
        {
            var vacancyposting = await _context.VacancyPosting
            .SingleOrDefaultAsync(x => x.VACANCYID == Id);
            return vacancyposting;
        }

        public void AddNewVacancy(VacancyPosting vacancy)
        {
            _context.Entry(vacancy).State = EntityState.Added;

        }

        public async Task<VacancyPostingDto> GetVacancyDetailsById(int Id)
        {
            var vacancyData = await (from v in _context.VacancyPosting
                                     where v.VACANCYID == Id
                                     select new VacancyPostingDto
                                     {
                                         VACANCYID = v.VACANCYID,
                                         GRADE = v.GRADE,
                                         JOBTITLE = v.JOBTITLE,
                                         DEPARTMENT = v.DEPARTMENT,
                                         LOCATION = v.LOCATION,
                                         JOBTYPE = v.JOBTYPE,
                                         JOBDESC = !string.IsNullOrEmpty(v.JOBDESC) ? _imageService.ConvertPdfToBase64(v.JOBDESC) : null,
                                         SALARYRANGE = v.SALARYRANGE,
                                         REQUIRMENTS = v.REQUIRMENTS,
                                         POSTEDDATE = v.POSTEDDATE,
                                         CLOSINGDATE = v.CLOSINGDATE,
                                         CONTACTINFO = v.CONTACTINFO,
                                         STATUS = v.STATUS,
                                         ISACTIVE = v.ISACTIVE,
                                         VACANCYCOUNT = v.VACANCYCOUNT,
                                         PDFPATH= v.JOBDESC
                                     }).OrderByDescending(s => s.VACANCYID).SingleOrDefaultAsync();
            return vacancyData;
        }

        public async Task<List<DepartmentDropdownDto>> GetDepartmentDropdown(string Name)
        {
            var datalist = await (from h in _context.Users
                                  where h.Department != null
                                  select new DepartmentDropdownDto
                                  {
                                      DepartmentName = h.Department,
                                  }).Distinct().ToListAsync();
            if (!string.IsNullOrEmpty(Name))
            {
                datalist = datalist.Where(d => d.DepartmentName.ToLower().Contains(Name.ToLower())).ToList();
            }
            return datalist;
        }

        public async Task<List<GradeDropdownDto>> GetGradeList(string Name)
        {

            var gradelist = await (from h in _context.Users
                                    where h.Grade != null
                                   select new GradeDropdownDto
                                   {
                                       Grade = h.Grade,
                                   }).Distinct().ToListAsync();
            if (!string.IsNullOrEmpty(Name))
            {
                gradelist = gradelist.Where(d => d.Grade.ToLower().Contains(Name.ToLower())).ToList();
            }
            return gradelist;
        }
    }
}