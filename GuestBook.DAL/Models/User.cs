using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Models
{
    public class User
    {
        public User()
        {
            this.Message = new HashSet<Message>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Pwd { get; set; }

        public string? Salt { get; set; }

        public ICollection<Message> Message { get; set; }
    }
}
