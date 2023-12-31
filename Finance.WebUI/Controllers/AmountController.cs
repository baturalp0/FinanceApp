﻿using Finance.Entities.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebUI.Controllers
{
    public class AmountController : Controller
    {
        Log log;

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
            var user = HttpContext.Session.GetString("nick_name");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("nick_name")))
            {
                return RedirectToAction("Login", "User");
            }

            var user_id = HttpContext.Session.GetInt32("id");
            var amounts = _context.Amounts.Where(x => x.user_id == user_id).ToList();

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
                var user_id = HttpContext.Session.GetInt32("id");
                model.user_id = Convert.ToInt32(user_id);
                //Amount eklerken gelir mi gider mi kontrolü. Ona göre true ya da false olarak aldık.
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


                log = new Log() { action_name = "Add", controller_name = "Amount", message = "Başarılı İşlem Ekleme", user_id = model.user_id };
                _context.Logs.Add(log);
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

            var user_id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            log = new Log() { action_name = "Delete", controller_name = "Amount", message = "Başarılı Kayıt Silme", user_id = user_id };
            _context.Logs.Add(log);
            _context.SaveChanges();

            return RedirectToAction("List");
        }




        [HttpPost]
        public IActionResult Edit(int amount1, string name1, int id1, bool type)
        {
            var existAmount = _context.Amounts.FirstOrDefault(x => x.id == id1);
            if (existAmount != null)
            {
                existAmount.amount = amount1;
                existAmount.name = name1;
                existAmount.type_ = type;
                _context.SaveChanges();

                TempData["SuccessSave"] = "Kayıt Başarılı";

                var user_id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                log = new Log() { action_name = "Edit", controller_name = "Amount", message = "Başarılı Kayıt Güncelleme", user_id = user_id };
                _context.Logs.Add(log);
                _context.SaveChanges();

                return RedirectToAction("List");
            }
            return View();
        }


    }
}
