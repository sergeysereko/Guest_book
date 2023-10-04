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
    public class UserRepository :IRepository<User>
    {
        private GuestBookContext db;

        public UserRepository(GuestBookContext context)
        {
            this.db = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> Get(string name)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }

        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }

        public async Task<User> Get(int id)
        {
            User? user = await db.Users.FindAsync(id);
            return user;
        }
    }
}
