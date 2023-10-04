using GuestBook.BLL.DTO;
using GuestBook.BLL.Interfaces;
using GuestBook.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuestBook.DAL.Models;
using GuestBook.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Numerics;
using System.Net.Http;



namespace GuestBook.BLL.Services
{
    public class MessageService:IMessageService
    {
        IUnitOfWorks Database { get; set; }


        public MessageService(IUnitOfWorks uow)
        {
            Database = uow;
        }


        public async Task CreateMessage(MessageDTO messageDto)
        {   var user = new User();
            user = await Database.Users.Get(messageDto.Id_User);
            var message = new Message
            {
                Id = messageDto.Id,
                Message_text = messageDto.Message_text,
                MessageDate = messageDto.MessageDate,
                Id_User = messageDto.Id_User,
                User = user

            };
            await Database.Messages.Create(message);
            await Database.Save();
        }


        public async Task<MessageDTO> GetMessage(string message)
        {
            throw new NotImplementedException();// заглушака чтобы интерфейс не ругался

        }

        public async Task<IEnumerable<MessageDTO>> GetMessage()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>()
            .ForMember("User", opt => opt.MapFrom(c=>c.User.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(await Database.Messages.GetAll());
        }
    }
}

