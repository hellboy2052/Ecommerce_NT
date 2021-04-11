using Microsoft.AspNetCore.Mvc;

namespace ServerSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
