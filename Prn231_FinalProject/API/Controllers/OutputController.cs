using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputController : ControllerBase
    {
        private QuanLyKhoContext _context;
        public OutputController(QuanLyKhoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.OutputInfos.Include(o => o.IdOutputInfoNavigation).Include(o => o.IdObjectNavigation).ToList());
        }
    }
}
