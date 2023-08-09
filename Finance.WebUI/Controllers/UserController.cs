using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Finance.WebUI.Controllers
{
    public class UserController : Controller
    {
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
            _context.Users.Add(model);
            _context.SaveChanges();
            return RedirectToAction("List");
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
                TempData["SuccessMessage"] = "Giriş Başarılı";
            }
            else
            {
                TempData["ErrorMessage"] = "Geçersiz kullanıcı adı veya şifre";
            }
            return RedirectToAction("Login"); // Aynı view'a yönlendiriyoruz
        }

    }

}

