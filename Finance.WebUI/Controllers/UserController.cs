﻿using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Finance.WebUI.Controllers
{
    public class UserController : Controller
    {
        Log log;

        private readonly DatabaseContext _context;
        //readOnly bu değişkene sadece burada değer atanabileceğini gösterir. Sanırım bu _context Dependency Injectionla alakalı.

        public UserController(DatabaseContext context)
        { //Dependency Injection -> mülakat sorusu.
            _context = context;
        }

        public IActionResult List()
        {
            var users = _context.Users.Where(x => x.id == 1).ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                model.role_id = 2;
                _context.SaveChanges();

                log = new Log() { action_name = "Create", controller_name = "User", message = "Başarılı Kullanıcı Kayıt", user_id = model.id };
                _context.Logs.Add(log);
                _context.SaveChanges();

                return RedirectToAction("List", "Amount");

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {


            var user = _context.Users.FirstOrDefault(x => x.email == model.email && x.password == model.password);

            if (user != null)
            {
                //Session'a bilgileri atıyoruz.
                HttpContext.Session.SetInt32("id", user.id);
                HttpContext.Session.SetString("nick_name", user.nick_name);
                HttpContext.Session.SetString("password", user.password);
                HttpContext.Session.SetString("email", user.email);
                HttpContext.Session.SetInt32("role_id", user.role_id);

                log = new Log() { action_name = "Login", controller_name = "User", message = "Başarılı Giriş", user_id = user.id };
                _context.Logs.Add(log);
                _context.SaveChanges();

                if (user.role_id == 1)
                {
                    return RedirectToAction("List", "Admin"); // Aynı view'a yönlendiriyoruz
                }
                else
                {
                    return RedirectToAction("List", "Amount"); // Aynı view'a yönlendiriyoruz
                }


            }
            else
            {
                TempData["ErrorMessage"] = "Geçersiz mail adresi veya şifre";
                log = new Log() { action_name = "Login", controller_name = "User", message = "Başarısız Giriş", user_id = 0 };
                _context.Logs.Add(log);
                _context.SaveChanges();
                return RedirectToAction("Login"); // Aynı view'a yönlendiriyoruz
            }
        }

        public IActionResult Logout()
        {
            var user_id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

            log = new Log() { action_name = "Logout", controller_name = "User", message = "Başarılı Çıkış", user_id = user_id };
            _context.Logs.Add(log);
            _context.SaveChanges();


            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("nick_name");
            HttpContext.Session.Remove("password");
            HttpContext.Session.Remove("email");



            //HttpContext.Session.Abandon();
            //FormsAuthentication.SignOut();


            return RedirectToAction("Login");
        }

        public IActionResult GoToLogin()
        {
            return RedirectToAction("Create");
        }

        public IActionResult GoToCreate()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            return View();
        }

        //Burası profil sayfasını güncellediğimiz yer. Burada yapılan değişiklikler session için de yapılmalı.
        [HttpPost]
        public IActionResult EditUser(string nick_name, string email, string password, int currentId, int currentRoleId)
        {
            var existUser = _context.Users.FirstOrDefault(x => x.id == currentId);

            if (existUser != null)
            {
                existUser.nick_name = nick_name;
                existUser.email = email;
                existUser.password = password;
                _context.SaveChanges();

                HttpContext.Session.SetString("nick_name", nick_name);
                HttpContext.Session.SetString("password", password);
                HttpContext.Session.SetString("email", email);

                var user_id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                log = new Log() { action_name = "EditUser", controller_name = "User", message = "Başarılı Kullanıcı Güncelleme", user_id = user_id };
                _context.Logs.Add(log);
                _context.SaveChanges();

                return RedirectToAction("Profile");
            }
            return View();
        }

    }

}

