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
            return Ok(_context.OutputInfos.Include(o => o.IdOutputNavigation).Include(o => o.IdObjectNavigation).ToList());
        }

        [HttpPost("api/CreateNewOutPut")]
        public IActionResult CreateNewOutPut(Output output)
        {
            try
            {

                _context.Outputs.Add(output);
                _context.SaveChanges();
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(521, "Can't add new ");
            }
        }
        [HttpPost("api/CreateNewOutPutInfos")]
        public IActionResult CreateNewOutPutInfos(OutputInfo output)
        {
            try
            {

                _context.OutputInfos.Add(output);
                _context.SaveChanges();
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(521, "Can't add new ");
            }
        }
        [HttpPut("api/UpdateOutputInfo/{id}")]
        public IActionResult UpdateOutputInfo(string id, OutputInfo output)
        {
            try
            {
                OutputInfo OutputIf = _context.OutputInfos.Where(o => o.Id.Equals(id)).FirstOrDefault();
                if (OutputIf != null)
                {
                    OutputIf.IdCustomer = output.IdCustomer;
                    OutputIf.IdObject = output.IdObject;
                    OutputIf.Count = output.Count;
                    OutputIf.IdInputSelct = output.IdInputSelct;
                    OutputIf.Status = output.Status;
                    _context.OutputInfos.Update(OutputIf);
                    _context.SaveChanges();
                    return NoContent();

                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(521, "Can't add new ");
            }

        }
    }
}
