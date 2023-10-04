using GuestBook.DAL.Interfaces;
using GuestBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Interfaces
{
    public interface IUnitOfWorks
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        Task Save();
    }
}
