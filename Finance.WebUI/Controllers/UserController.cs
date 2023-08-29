using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Finance.WebUI.Controllers
{
    public class UserController : Controller
    {
        Log log;

        private readonly DatabaseContext _context; //Global değişkenler _ ile başlasa iyi olur. Best Practice'e göre bu şekilde.
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
                var exist = _context.Users.Where(x => x.email == model.email).FirstOrDefault();
                var exist1 = _context.Users.Any(x => x.email == model.email); //true ya da false döner.
                if (exist1 == true)
                {
                    TempData["InfoMessage"] = $"{model.email} daha önce alınmış.";
                    // return View(model); //model dönersek iinputlarda model durmaya devam eder. View() dönerse inputların içi silinir.
                }
                else
                {
                    _context.Users.Add(model);
                    model.role_id = 2;
                    log = new Log() { action_name = "Create", controller_name = "User", message = "Başarılı Kullanıcı Kayıt", user_id = model.id };
                    _context.Logs.Add(log);
                    _context.SaveChanges();

                    return RedirectToAction("List", "Amount");

                }


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
                HttpContext.Session.SetString("PictureFilePath", user.ProfilePicturePath);

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
            var existEmail = _context.Users.FirstOrDefault(x => x.email == email);

            if (existUser != null)
            {

                if (existEmail != null)
                {
                    TempData["InfoMessage"] = "Success";
                    return Json("2");
                }
                else
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

                    return Json("1");

                }

            }
            return Json("3");
        }


        [HttpPost]
        public IActionResult UploadProfilePicture(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName); // Dosya adını alın
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Resim dosya yolunu veritabanına kaydetme
                var userId = HttpContext.Session.GetInt32("id"); // Kullanıcının kimliği
                var user = _context.Users.FirstOrDefault(x => x.id == userId);

                if (user != null)
                {
                    user.ProfilePicturePath = "/uploads/" + fileName; // Veritabanında sadece resim dosya adını kaydet
                    HttpContext.Session.SetString("PictureFilePath", user.ProfilePicturePath);
                    _context.SaveChanges();
                }

                return Json(new { success = true, message = "Resim başarıyla yüklendi" });
            }

            return Json(new { success = false, message = "Resim yüklenirken bir hata oluştu" });
        }




    }

}

