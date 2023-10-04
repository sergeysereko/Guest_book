using GuestBook.DAL.Context;
using GuestBook.DAL.Interfaces;
using GuestBook.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Repositories
{
    internal class MessageRepository : IRepository<Message>
    {
        private GuestBookContext db;

        public MessageRepository(GuestBookContext context)
        {
            this.db = context;
        }

        public async Task<List<Message>> GetAll()
        {
            var gbContext = db.Messages.Include(p => p.User);
            return await gbContext.ToListAsync();
        }

        public async Task<Message> Get(string name) 
        {
            throw new NotImplementedException();// заглушака чтобы интерфейс не ругался
        }

        public async Task Create(Message message)
        {
            await db.Messages.AddAsync(message);
        }

        public async Task<Message> Get(int id)
        {
            var messages = await db.Messages.Include(o => o.User).Where(a => a.Id == id).ToListAsync();
            Message? message = messages?.FirstOrDefault();
            return message;
        }
    }
}
