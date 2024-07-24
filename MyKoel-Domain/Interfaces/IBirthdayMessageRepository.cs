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
    public interface IBirthdayMessageRepository
    {
        void UpdateMessage(BirthdayMessage message);
        Task<bool> SaveAllAsync();
        void DeleteMessage(BirthdayMessage message);
        Task<BirthdayMessage> GetMessageById(int Id);
        void AddNewMessage(BirthdayMessage message);
        Task<bool> MessageExists(int Day);
        Task<PagedList<BirthdayMessageDto>> BirthdayMessageList(ParameterParams parameterParams);
    }
}