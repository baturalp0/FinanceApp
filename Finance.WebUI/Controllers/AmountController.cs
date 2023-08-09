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
            var amounts = _context.Amounts.Where(x => x.id == 1).ToList();
            return View(amounts);
        }



    }
}
