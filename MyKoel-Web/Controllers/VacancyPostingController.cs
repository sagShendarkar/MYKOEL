using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using AutoMapper;
using iot_Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using MyKoel_Domain.Data;
using MyKoel_Domain.DTOs;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Models.Masters;

namespace MyKoel_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacancyPostingController : ControllerBase
    {
        private readonly IVacancyPosting _vacancyRepository;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VacancyPostingController(IVacancyPosting vacancyRepository, IMapper mapper, DataContext context,IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _vacancyRepository = vacancyRepository;
            _config=config;
        }

        [HttpGet("ShowVacancyList")]
        public async Task<List<VacancyPostingDto>> ShowSectionList([FromQuery] ParameterParams parameterParams)
        {
            var VacancyList = await _vacancyRepository.GetVacancyList(parameterParams);
            Response.AddPaginationHeader(VacancyList.CurrentPage, VacancyList.PageSize,
                    VacancyList.TotalCount, VacancyList.TotalPages);
            return VacancyList;
        }

        [HttpGet("GetVacancyById/{Id}")]
        public async Task<ActionResult<VacancyPostingDto>> GetVacancyById(int Id)
        {
            var data = await _vacancyRepository.GetVacancyDetailsById(Id);
            return data;
        }
        [HttpPost("AddVacancy")]
        public async Task<object> AddVacancy(VacancyPostingDto vacancy)
        {
            try
            {
                var vacancydetails = _mapper.Map<VacancyPosting>(vacancy);

                if (vacancy.FILESTRING != null)
                {
                    string rootFolderPath = _config.GetValue<string>("RootFolderPath");

                    if (!Directory.Exists(rootFolderPath))
                    {
                        Directory.CreateDirectory(rootFolderPath);
                    }

                    string folderPath = Path.Combine(rootFolderPath, "Job Description");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string userFolderPath = Path.Combine(folderPath, vacancy.GRADE);

                    if (!Directory.Exists(userFolderPath))
                    {
                        Directory.CreateDirectory(userFolderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + vacancy.FILETYPE;
                    string imagePath = Path.Combine(userFolderPath, fileName);

                    string base64StringData = vacancy.FILESTRING;
                    string cleandata = base64StringData.Substring(base64StringData.IndexOf(',') + 1);
                    byte[] data = Convert.FromBase64String(cleandata);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                        {
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                    }

                    vacancy.JOBDESC = Path.Combine(userFolderPath, fileName);
                }
                var vacancydata = _mapper.Map<VacancyPosting>(vacancy);
                _vacancyRepository.AddNewVacancy(vacancydata);
                if (await _vacancyRepository.SaveAllAsync())
                {
                    return new
                    {
                        Status = 200,
                        Message = "Data Saved Successfully"
                    };
                }
                else
                {
                    return new
                    {
                        Status = 400,
                        Message = "Failed To Save Data"
                    };

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = $"Failed to update section: {ex.Message}"
                });
            }
        }

        [HttpPost("UpdateVacancy")]
        public async Task<IActionResult> UpdateSection(VacancyPostingDto vacancyDto)
        {
            try
            {
                var existingSection = await _vacancyRepository.GetVacancyById(vacancyDto.VACANCYID);

                if (existingSection == null)
                {
                    return NotFound("Section not found");
                }
                if (vacancyDto.FILESTRING != null)
                {
                    string rootFolderPath = _config.GetValue<string>("RootFolderPath");

                    if (!Directory.Exists(rootFolderPath))
                    {
                        Directory.CreateDirectory(rootFolderPath);
                    }

                    string folderPath = Path.Combine(rootFolderPath, "Job Description");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string userFolderPath = Path.Combine(folderPath, vacancyDto.GRADE);

                    if (!Directory.Exists(userFolderPath))
                    {
                        Directory.CreateDirectory(userFolderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + vacancyDto.FILETYPE;
                    string imagePath = Path.Combine(userFolderPath, fileName);

                    string base64StringData = vacancyDto.FILESTRING;
                    string cleandata = base64StringData.Substring(base64StringData.IndexOf(',') + 1);
                    byte[] data = Convert.FromBase64String(cleandata);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                        {
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                    }

                    vacancyDto.JOBDESC = Path.Combine(userFolderPath, fileName);
                }

                var updatedvacancy = _mapper.Map(vacancyDto, existingSection);
                _vacancyRepository.UpdateVacancy(updatedvacancy);
                if (await _vacancyRepository.SaveAllAsync())
                {
                    return Ok(new
                    {
                        Status = 200,
                        Message = "Section updated successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = 400,
                        Message = "Failed To Update Data",

                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = $"Failed to update section: {ex.Message}"
                });
            }
        }

        [HttpPost("DeleteVacancy/{Id}")]
        public async Task<ActionResult> DeleteCompanyDetails(int Id)
        {

            var data = await _vacancyRepository.GetVacancyById(Id);
            try
            {
                _vacancyRepository.DeleteVacancy(data);
                if (await _vacancyRepository.SaveAllAsync())
                {
                    var result = new
                    {
                        Status = 200,
                        Message = "Data Deleted Successfully"
                    };
                    return Ok(result);
                }
                return BadRequest("Failed To Delete Data");

            }
            catch (Exception ex)
            {
                var result = new
                {
                    Status = 400,
                    Message = ex.Message
                };
                return BadRequest(result);

            }

        }

        [HttpGet("GetDepartmentDD")]
        public async Task<ActionResult<List<DepartmentDropdownDto>>> GetDepartmentDD(string? Name)
        {
            var data = await _vacancyRepository.GetDepartmentDropdown(Name);
            return data;
        }

        [HttpGet("GetGradeDD")]
        public async Task<ActionResult<List<GradeDropdownDto>>> GetGradeDD(string? Name)
        {
            var data = await _vacancyRepository.GetGradeList(Name);
            return data;
        }

    }
}