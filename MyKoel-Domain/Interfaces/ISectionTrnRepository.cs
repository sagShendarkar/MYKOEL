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
    public interface ISectionTrnRepository
    {
          Task<PagedList<SectionTrnDto>> GetSectionList(ParameterParams parameterParams);
          void UpdateSection(SectionTransaction sectionTrn);
          Task<bool> SaveAllAsync();
          void DeleteSection(SectionTransaction sectionTransaction);
          Task<SectionTransaction> GetSectionById(int Id);
          void AddNewSection(SectionTransaction sectionTransaction);
          void UpdateAttachment(Attachments attachments);
          void AddAttachment(Attachments attachments);
          Task<Attachments> GetAttachmentById(int Id);

    }
}