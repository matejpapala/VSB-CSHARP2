using evidenceFirem.Models;
using evidenceFirem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace evidenceFirem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly DicService _dicService;

        public HomeController(DatabaseService databaseService, DicService dicService)
        {
            _databaseService = databaseService;
            _dicService = dicService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _databaseService.SelectCompanies();
            return View(model);
        }

        [HttpPost]
        public IActionResult Odstranit(int Id)
        {
            _databaseService.DeleteCompany(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Pridat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pridat(PridavaniViewModel model)
        {
            if(ModelState.IsValid)
            {
                JsonModel json = await _dicService.ValidateDic(model.Dic);
                if(json.valid)
                {
                    _databaseService.AddCompany(model);
                    return RedirectToAction("Index");
                }else
                {
                    ModelState.AddModelError("Dic", "Dic neni platne");
                }
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
