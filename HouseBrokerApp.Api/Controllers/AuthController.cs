using Microsoft.AspNetCore.Mvc;

namespace HouseBrokerApp.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
