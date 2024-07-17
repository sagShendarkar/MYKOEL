using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using iot_Domain.Helpers;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Domain.Interfaces
{
    public interface IVacancyPosting
    {
          Task<PagedList<VacancyPostingDto>> GetVacancyList(ParameterParams parameterParams);
          void UpdateVacancy(VacancyPosting vacancyPosting);
          Task<bool> SaveAllAsync();
          void DeleteVacancy(VacancyPosting vacancyPosting);
          Task<VacancyPosting> GetVacancyById(int Id);
          void AddNewVacancy(VacancyPosting sectionTransaction);
          Task<VacancyPostingDto> GetVacancyDetailsById(int Id);
          Task<List<DepartmentDropdownDto>> GetDepartmentDropdown(string Desc);
          Task<List<GradeDropdownDto>> GetGradeList(string Desc);

    }
}