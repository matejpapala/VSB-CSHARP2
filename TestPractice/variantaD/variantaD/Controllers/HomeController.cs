using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using variantaD.Models;
using variantaD.Services;

namespace variantaD.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SecondPage(FormModel model)
        {
            if (ModelState.IsValid)
            {
                string municipality = await _apiService.GetXmlApi(model.Psc);
                ModelState.Clear();
                SecondFormModel passedModel = new SecondFormModel
                {
                    Psc = model.Psc,
                    Municipality = municipality
                };
                return View(passedModel);
            }
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult SecondPage(SecondFormModel model)
        {
            if (ModelState.IsValid)
            {
                _apiService.SaveToFile(model);
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
