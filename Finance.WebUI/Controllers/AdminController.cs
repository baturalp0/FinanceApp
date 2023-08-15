using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Finance.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private readonly DatabaseContext _context;
        //readOnly bu değişkene sadece burada değer atanabileceğini gösterir. Sanırım bu _context Dependency Injectionla alakalı.
        public IActionResult Index()
        {
            return View();
        }

        public AdminController(DatabaseContext context)
        { //Dependency Injection -> mülakat sorusu.
            _context = context;
        }

        public IActionResult List()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("nick_name")))
            //{
            //    return RedirectToAction("Login", "User");
            //}

            //var user_id = HttpContext.Session.GetInt32("id");
            //var amounts = _context.Amounts.Where(x => x.user_id == user_id).ToList();

            var amounts = _context.Amounts.OrderBy(x => x.id).ToList(); //id'ye göre sıraladık

            //AmountViewModel viewModel = new AmountViewModel();
            //viewModel.AmountList = amounts;
            //viewModel.Amount = null;


            return View(amounts);
        }

        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            //UserViewModel viewModel = new UserViewModel();
            //viewModel.UserList = users;
            //viewModel.User = null;


            return View(users);
        }

        [HttpPost]
        public IActionResult Edit(int amount, string name, bool type, int currentId)
        {
            var existAmount = _context.Amounts.FirstOrDefault(x => x.id == currentId);
            if (existAmount != null)
            {
                existAmount.amount = amount;
                existAmount.name = name;
                existAmount.type_ = type;
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var amount = _context.Amounts.FirstOrDefault(x => x.id == id); //select * from category where id = '"+id+"'
            _context.Remove(amount);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.id == id); //select * from category where id = '"+id+"'
            _context.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }

    }
}
