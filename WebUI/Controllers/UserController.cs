using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
