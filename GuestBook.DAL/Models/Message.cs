using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Models
{
    public class Message
    {
        public int Id { get; set; }

     
        public string Message_text { get; set; }

        public DateTime MessageDate { get; set; }

        public int Id_User { get; set; }
        public User User { get; set; }
    }
}
