using Guest_book.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using GuestBook.BLL.DTO;
using GuestBook.BLL.Interfaces;
using GuestBook.BLL.Infrastructure;
using GuestBook.BLL.Services;
using Newtonsoft.Json;

namespace Guest_book.Controllers
{


    public class LoginController : Controller
    {

        private readonly IUserService userService;

        public LoginController(IUserService serv)
        {
            userService = serv;
        }


        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        [HttpPost]
        
        public async Task<IActionResult> Login()
        {
            Login logon = new Login();
            string userName = Request.Form["username"];
            string pass = Request.Form["password"];

            logon.UserName = userName;
            logon.Password = pass;

            if (ModelState.IsValid)
            {
                var users = await userService.GetUsers();
                if (users.Count() == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Json(new { success = false }); 
                }
                var usersf = users.Where(a => a.Name == logon.UserName);
                if (usersf.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Json(new { success = false }); 
                }
                var user = usersf.First();
                string? salt = user.Salt;

                string passw = logon.Password;
                var hash = await userService.GetHashCode(salt, passw);

                if (user.Pwd != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Json(new { success = false }); 
                }
                HttpContext.Session.SetString("login", user.Name);

                return Json(new { success = true }); 
            }
            return Json(new { success = false }); 
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(Login logon)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var users = await userService.GetUsers();
        //        if (users.Count() == 0)
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //            return View(logon);
        //        }
        //        var usersf = users.Where(a => a.Name == logon.UserName);
        //        if (usersf.ToList().Count == 0)
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //            return View(logon);
        //        }
        //        var user = usersf.First();
        //        string? salt = user.Salt;

        //        string passw = logon.Password;
        //        var hash = await userService.GetHashCode(salt, passw);

        //        if (user.Pwd != hash.ToString())
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //            return View(logon);
        //        }
        //        HttpContext.Session.SetString("login", user.Name);

        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(logon);
        //}



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registr reg)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
                user.Name = reg.Login;
                user.Pwd = reg.Password;

                var passPlusSult = await userService.GetSaltAndPass(reg.Password);

                user.Pwd = passPlusSult.Password;
                user.Salt = passPlusSult.Salt;
                await userService.CreateUser(user);

                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
