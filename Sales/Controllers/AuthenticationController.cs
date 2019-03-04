using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sales.Models.ViewModels;
using Sales.Services;

namespace Sales.Controllers
{
    public class AuthenticationController : Controller
    {
        private AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            BaseViewModel model = await _authService.LoginWithGeneratedPromoCodeAsync(HttpContext);
            if(model.HaveError)
                return View("Index", model);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string promoCode)
        {
            BaseViewModel model = await _authService.LoginAsync(HttpContext, promoCode);
            if (model.HaveError)
                return View("Index", model);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _authService.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }

    }
}