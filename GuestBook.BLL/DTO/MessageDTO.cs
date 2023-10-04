using GuestBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Текст сообщения: ")]
        public string Message_text { get; set; }

        public DateTime MessageDate { get; set; }

        public int Id_User { get; set; }

        public string? User { get; set; }
    }
}
