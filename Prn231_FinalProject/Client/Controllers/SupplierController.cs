using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
