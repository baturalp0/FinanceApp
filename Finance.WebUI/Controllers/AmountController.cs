using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebUI.Controllers
{
    public class AmountController : Controller
    {
        private readonly DatabaseContext _context;
        //readOnly bu değişkene sadece burada değer atanabileceğini gösterir. Sanırım bu _context Dependency Injectionla alakalı.
        public IActionResult Index()
        {
            return View();
        }

        public AmountController(DatabaseContext context)
        { //Dependency Injection -> mülakat sorusu.
            _context = context;
        }

        public IActionResult List()
        {
            var amounts = _context.Amounts.Where(x => x.user_id == 1).ToList();

            AmountViewModel viewModel = new AmountViewModel();
            viewModel.AmountList = amounts;
            viewModel.Amount = null;


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Amount model, string actionType)
        {
            if (ModelState.IsValid)
            {
                if (actionType == "Gelir")
                {
                    model.type_ = true;
                }
                else if (actionType == "Gider")
                {
                    model.type_ = false;
                }
                _context.Amounts.Add(model);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(model);
        }


        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var amount = _context.Amounts.FirstOrDefault(x => x.id == id); //select * from category where id = '"+id+"'
            _context.Remove(amount);
            _context.SaveChanges();
            return RedirectToAction("List");
        }


    }
}
