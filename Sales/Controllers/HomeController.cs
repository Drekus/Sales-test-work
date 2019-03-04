using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sales.Models.ViewModels;
using Sales.Services;

namespace Sales.Controllers
{
    public class HomeController : Controller
    {
        private SalesService _salesService;

        public HomeController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _salesService.GetModel(HttpContext, page);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrder(int bookId, int page = 1)
        {
            var model = await _salesService.AddToOrder(HttpContext, page, bookId);
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromOrder(int bookId, int page = 1)
        {
            var model = await _salesService.RemoveFromOrder(HttpContext, page, bookId);
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Сheckout(int page = 1)
        {
            var model = await _salesService.Сheckout(HttpContext, page);
            return View("Index", model);
        }
    }
}
