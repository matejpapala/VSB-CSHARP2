using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using variantaF.Models;
using variantaF.Services;

namespace variantaF.Controllers
{
    public class HomeController : Controller
    {
        private readonly MenzaService _menzaService;
        private readonly DatabaseService _databaseService;

        public HomeController(MenzaService menza, DatabaseService databaseService)
        {
            _menzaService = menza;
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new InputViewModel());
        }

        [HttpPost]
        public IActionResult Index(InputViewModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Menza", new { Date = model.Date });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Menza(string Date)
        {
            List<XmlModel> meals = await _menzaService.MenzaPost(Date);
            return View(meals);
        }

        [HttpPost]
        public IActionResult Menza(XmlModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Meal");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Meal(string Date, string altId)
        {
            FormViewModel model = new FormViewModel()
            {
                Date = Date,
                AltId = altId,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Meal(FormViewModel model)
        {
            if(ModelState.IsValid)
            {
                _databaseService.SaveToDatabase(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
