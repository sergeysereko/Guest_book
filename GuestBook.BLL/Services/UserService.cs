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
using System.Security.Cryptography;

namespace GuestBook.BLL.Services
{
    public class UserService:IUserService
    {
        IUnitOfWorks Database { get; set; }

        public UserService(IUnitOfWorks uow)
        {
            Database = uow;
        }

        public async Task CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Pwd = userDto.Pwd,
                Salt = userDto.Salt
            };
            await Database.Users.Create(user);
            await Database.Save();
        }

        public async Task<UserDTO> GetUser(string name)
        {
            var user = await Database.Users.Get(name);
            if (user == null)
            {
                throw new ValidationException("Wrong user!");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Pwd = user.Pwd,
                Salt = user.Salt
                
            };
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }



        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);
            if (user == null)
            {
                throw new ValidationException("Wrong player!");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Pwd = user.Pwd,
                Salt = user.Salt,
            };
        }

        public async Task <StringBuilder> GetHashCode(string salt, string pasw)
        {
            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + pasw);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            }

            return hash;
        }


        public async Task<(string Salt, string Password)> GetSaltAndPass(string pass)
        {

            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + pass);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            }
            string passsalt = hash.ToString();

            return (salt, passsalt);
        }
    }
}

