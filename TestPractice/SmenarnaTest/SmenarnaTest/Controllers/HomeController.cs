using Microsoft.AspNetCore.Mvc;
using SmenarnaTest.Models;
using SmenarnaTest.Services;
using System.Diagnostics;

namespace SmenarnaTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CurrencyServices _currencyServices;
        private readonly DatabaseService _databaseService;

        public HomeController(CurrencyServices currencyServices, DatabaseService databaseService)
        {
            _currencyServices = currencyServices;
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _currencyServices.GetCourses();

            ViewBag.Meny = data.Kurzy.Keys;

            return View(new ExchangeFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> ZpracujSmenu(ExchangeFormModel model)
        {
            if(!ModelState.IsValid)
            {
                var kurzyZnovu = await _currencyServices.GetCourses();
                ViewBag.Meny = kurzyZnovu.Kurzy.Keys;
                return View("Index", model);
            }

            var data = await _currencyServices.GetCourses();
            double kurz = data.Kurzy[model.Currency].DevStred;
            double vysledek = model.Value * kurz;
            _databaseService.UlozSmenu(model, vysledek);
            return RedirectToAction("Historie");
        }

        [HttpGet]
        public IActionResult Historie()
        {
            var smeny = _databaseService.VypisSmeny();
            return View(smeny);
        }
    }
}
