using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class InputController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
