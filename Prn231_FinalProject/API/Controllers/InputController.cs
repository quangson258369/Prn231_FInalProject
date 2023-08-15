using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputController : ControllerBase
    {
        private QuanLyKhoContext _context;
        public InputController(QuanLyKhoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.InputInfos.Include(o => o.IdInputNavigation).Include(o => o.IdObjectNavigation).ToList());
        }
    }
}
