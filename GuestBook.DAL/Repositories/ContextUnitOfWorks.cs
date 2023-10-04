using GuestBook.DAL.Context;
using GuestBook.DAL.Interfaces;
using GuestBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Repositories
{
    public class ContextUnitOfWorks:IUnitOfWorks
    {
        private GuestBookContext db;
        private UserRepository userRepository;
        private MessageRepository messageRepository;

        public ContextUnitOfWorks(GuestBookContext context)
        {
            db = context;
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(db);
                return messageRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}
