using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class OutputController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
