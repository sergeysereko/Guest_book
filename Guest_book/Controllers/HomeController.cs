using Microsoft.AspNetCore.Mvc;
using Guest_book.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using GuestBook.BLL.DTO;
using GuestBook.BLL.Interfaces;
using GuestBook.BLL.Infrastructure;
using Newtonsoft.Json;
using GuestBook.BLL.Services;

namespace Guest_book.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        public HomeController(IMessageService messageserv, IUserService userserv)
        {
            messageService = messageserv;
            userService = userserv;
        }


        // GET: Messages
        //public async Task<IActionResult> Index()
        //{
        //    var model = await messageService.GetMessage();
        //    return View(model);
            
        //}

        public async Task<IActionResult> Index()
        {   
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await messageService.GetMessage();

            if (messages == null)
            {
                return Problem("Список сообщений пустой");
            }

            List<MessageDTO> list = messages.ToList(); 
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        
        [HttpGet]
        public IActionResult Create()
        {      
            return View();
        }


    
    [HttpPost]
    public async Task<IActionResult>CreateMessage()
    {
            string messageText = Request.Form["message"];
            MessageDTO message = new MessageDTO();
           
            string login = HttpContext.Session.GetString("login");
        if (!string.IsNullOrEmpty(login))
        {
            var user = await userService.GetUser(login);
            if (user != null)
            {
                int userId = user.Id;
                message.Message_text = messageText;
                message.MessageDate = DateTime.Now;
                message.Id_User = userId;
                message.User = user.Name;
            }
        }

        if (ModelState.IsValid)
        {
            await messageService.CreateMessage(message);

            string response = "Сообщение успешно добавлено!";
            return Json(response);
        }
        return Problem("Проблемы при добавлении студента!");
    }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(MessageDTO message)
        //{
        //    string login = HttpContext.Session.GetString("login");
        //    if (!string.IsNullOrEmpty(login))
        //    {
        //        var user = await userService.GetUser(login);
        //        if (user != null)
        //        {
        //            int userId = user.Id;
        //            message.MessageDate = DateTime.Now;
        //            message.Id_User = userId;
        //            message.User = user.Name;
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await messageService.CreateMessage(message);
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View(message);
        //}




    }
}



