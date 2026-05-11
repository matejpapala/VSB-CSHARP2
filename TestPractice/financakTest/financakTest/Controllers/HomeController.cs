using financakTest.Models;
using financakTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace financakTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinancakService _financakService;
        private readonly DataStorage _dataStorage;

        public HomeController(FinancakService financakService, DataStorage dataStorage)
        {
            _financakService = financakService;
            _dataStorage = dataStorage;
        }
        public async Task<IActionResult> Index()
        {
            var urady = await _financakService.GetXml();
            return View(urady);
        }

        public IActionResult Detail(string id)
        {
            ViewBag.PassedValue = id;
            return View();
        }

        [HttpPost]
        public IActionResult Detail(string id, DetailViewModel model)
        {

            if(ModelState.IsValid)
            {
                model.CHodnota = id;

                _dataStorage.SaveToFile(model, "data.txt");
                return RedirectToAction("Index");
            }

            ViewBag.PassedValue = id;
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
