using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebUI.Controllers
{
    //readonly mülakat sorusudur. Bil.
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        { //Dependency Injection -> mülakat sorusu.
            _context = context;
        }

        public IActionResult List() //Burası bize tabloyu liste olarak dönen kısım denebilir.
        {
            var categories = _context.Categories.Where(x=>x.user_id==1).ToList();
            return View(categories);
        }

        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id); //Find daha hızlı. Find ve FirstOrDefault yaptıkları işlemler aynı. Asnotracking olması sebebi. 
            return View(category);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category model)
        {
            var category = _context.Categories.FirstOrDefault(x => x.id == model.id);
            category.name= model.name;
            category.user_id = model.user_id;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.id == id); //select * from category where id = '"+id+"'
            _context.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();
            return RedirectToAction("List");
        }


    }
}
