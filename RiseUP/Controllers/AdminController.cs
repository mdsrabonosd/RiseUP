using Microsoft.AspNetCore.Mvc;

namespace RiseUP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
