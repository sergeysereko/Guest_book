using GuestBook.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task<UserDTO> GetUser(string name);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(int id);

        Task<StringBuilder> GetHashCode(string salt, string pasw);

        Task<(string Salt, string Password)> GetSaltAndPass(string pass);
    }
}
