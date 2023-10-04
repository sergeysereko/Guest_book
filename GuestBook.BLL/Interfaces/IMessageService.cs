using GuestBook.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(MessageDTO messageDto);
        Task<MessageDTO> GetMessage(string message);
        Task<IEnumerable<MessageDTO>> GetMessage();
    }
}
