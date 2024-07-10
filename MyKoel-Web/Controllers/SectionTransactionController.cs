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
    public class SectionTransactionController : ControllerBase
    {
        private readonly ISectionTrnRepository _sectionTrnRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionTransactionController(ISectionTrnRepository sectionTrnRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _sectionTrnRepository = sectionTrnRepository;
        }

        [HttpGet("ShowSectionList")]
        public async Task<List<SectionTrnDto>> ShowSectionList([FromQuery] ParameterParams parameterParams)
        {
            var company = await _sectionTrnRepository.GetSectionList(parameterParams);
            Response.AddPaginationHeader(company.CurrentPage, company.PageSize,
                    company.TotalCount, company.TotalPages);

            return company;
        }

        [HttpGet("GetSectionTrnById/{Id}")]
        public async Task<ActionResult<AddSectionTrnDto>> GetSectionDetailsById(int Id)
        {

            var data = await _sectionTrnRepository.GetSectionDetailsById(Id);
            return data;
        }
        [HttpPost("AddSection")]
        public async Task<object> AddSection(AddSectionTrnDto sectionTrnDto)
        {
            try
            {
                var section = _mapper.Map<SectionTransaction>(sectionTrnDto);
                _sectionTrnRepository.AddNewSection(section);
                if (await _sectionTrnRepository.SaveAllAsync())
                {
                    foreach (var item in sectionTrnDto.Attachment)
                    {
                        if (item.IMAGESRC != null)
                        {
                            string rootFolderPath = @"C:\MyKoelImages";

                            if (!Directory.Exists(rootFolderPath))
                            {
                                Directory.CreateDirectory(rootFolderPath);
                            }
                            string folderPath = "";
                            if (sectionTrnDto.FLAG == "Announcement")
                            {
                                folderPath = Path.Combine(rootFolderPath, "Announcements");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                            }
                            if (sectionTrnDto.FLAG == "News")
                            {
                                folderPath = Path.Combine(rootFolderPath, "Company News");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                            }
                            if (sectionTrnDto.FLAG == "New Hires")
                            {
                                folderPath = Path.Combine(rootFolderPath, "New Hires");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                            }

                            string fileName = Guid.NewGuid().ToString() + ".png";
                            string imagePath = Path.Combine(folderPath, fileName);

                            string base64StringData = item.IMAGESRC;
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

                            item.FILENAME = fileName;
                            item.ISACTIVE = true;
                            item.PATH = Path.Combine(folderPath, fileName);
                            item.SECTIONID = section.SECTIONID;
                            var attachments = _mapper.Map<Attachments>(item);
                            _sectionTrnRepository.AddAttachment(attachments);


                        }
                    }
                    if (await _sectionTrnRepository.SaveAllAsync())
                    {
                        return new
                        {
                            Status = 200,
                            Message = "Data Saved Successfully",
                            SectionId = sectionTrnDto.SECTIONID,
                            Title = sectionTrnDto.TITLE
                        };
                    }
                }
                return new
                {
                    Status = 200,
                    Message = "Data Saved Successfully"
                };


            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add/update data: " + ex.Message);
            }
        }

        [HttpPost("UpdateSection")]
        public async Task<IActionResult> UpdateSection(AddSectionTrnDto sectionDto)
        {
            try
            {
                var existingSection = await _sectionTrnRepository.GetSectionById(sectionDto.SECTIONID);

                if (existingSection == null)
                {
                    return NotFound("Section not found");
                }

                var updatedsection = _mapper.Map(sectionDto, existingSection);
                _sectionTrnRepository.UpdateSection(updatedsection);
                if (await _sectionTrnRepository.SaveAllAsync())
                {
                    if (sectionDto.Attachment != null)
                    {
                        foreach (var item in sectionDto.Attachment)
                        {
                            var attachment = await _sectionTrnRepository.GetAttachmentById(item.ATTACHMENTID);

                            if (item.IMAGESRC != null)
                            {
                                string rootFolderPath = @"C:\MyKoelImages";
                                if (!Directory.Exists(rootFolderPath))
                                {
                                    Directory.CreateDirectory(rootFolderPath);
                                }
                                string folderPath = "";
                                if (sectionDto.FLAG == "Announcement")
                                {
                                    folderPath = Path.Combine(rootFolderPath, "Announcements");

                                    if (!Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }
                                }
                                if (sectionDto.FLAG == "News")
                                {
                                    folderPath = Path.Combine(rootFolderPath, "Company News");

                                    if (!Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }
                                }
                                if (sectionDto.FLAG == "New Hires")
                                {
                                    folderPath = Path.Combine(rootFolderPath, "New Hires");

                                    if (!Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }
                                }

                                string fileName = Guid.NewGuid().ToString() + ".png";
                                string imagePath = Path.Combine(folderPath, fileName);

                                string base64StringData = item.IMAGESRC;
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
                                item.FILENAME = fileName;
                                item.ISACTIVE = true;
                                item.PATH = Path.Combine(folderPath, fileName);

                                if (item.ATTACHMENTID == 0)
                                {
                                    var attachments = _mapper.Map<Attachments>(item);
                                    _sectionTrnRepository.AddAttachment(attachments);
                                }
                                else
                                {
                                    if (attachment == null)
                                    {
                                        return NotFound("attachment not found");
                                    }
                                    item.FILENAME = fileName;
                                    item.PATH = Path.Combine(folderPath, fileName);
                                    attachment = _mapper.Map(item, attachment);
                                    _sectionTrnRepository.UpdateAttachment(attachment);

                                }

                                if (await _sectionTrnRepository.SaveAllAsync())
                                {
                                    return Ok(new
                                    {
                                        Status = 200,
                                        Message = "Section updated successfully",
                                        SectionId = sectionDto.SECTIONID,
                                        Title = sectionDto.TITLE
                                    });
                                }
                                else
                                {
                                    return BadRequest(new
                                    {
                                        Status = 400,
                                        Message = "Failed to save changes"
                                    });
                                }
                            }

                            else
                            {
                                var attachmentdata = _mapper.Map(item, attachment);
                                _sectionTrnRepository.UpdateAttachment(attachmentdata);
                            }
                        }
                    }
                }
                if(await _sectionTrnRepository.SaveAllAsync()){
                return Ok(new
                {
                    Status = 200,
                    Message = "Section updated successfully",
                    SectionId = sectionDto.SECTIONID,
                    Title = sectionDto.TITLE
                });
                }
                else{
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

        [HttpPost("DeleteSection/{Id}")]
        public async Task<ActionResult> DeleteCompanyDetails(int Id)
        {

            var data = await _sectionTrnRepository.GetSectionById(Id);
            try
            {
                _sectionTrnRepository.DeleteSection(data);
                if (await _sectionTrnRepository.SaveAllAsync())
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

    }
}