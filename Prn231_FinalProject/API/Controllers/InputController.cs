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
        [HttpGet("GetAllInputs")]
        public IActionResult GetAllInputs()
        {
            try
            {
                var inputs = _context.Inputs
                    .ToList();

                return Ok(inputs); // Return the list of Input records as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving Input records.");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<List<Input>> GetInputAndInputInforById(string id)
        {

            var inputAndInputInfo = _context.InputInfos
                .Where(input => input.IdInput == id)
                .ToList();
            if (inputAndInputInfo == null)
            {
                return NotFound();
            }

            return Ok(inputAndInputInfo);
        }

        [HttpPost("CreateNewInPut")]
        public IActionResult CreateNewInPut(Input input)
        {
            try
            {
                _context.Inputs.Add(input);
                _context.SaveChanges();
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(521, "Can't add new ");
            }
        }
        [HttpPost("CreateNewInPutInfo")]
        public IActionResult CreateNewInPutInfos(InputInfo inputInfo)
        {
            try
            {

                _context.InputInfos.Add(inputInfo);
                _context.SaveChanges();
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(521, "Can't add new ");
            }
        }
        [HttpDelete("DeleteInput")]
        public IActionResult DeleteInput(string id)
        {
            try
            {
                // Find the Input to delete by its Id and DateInput
                var inputToDelete = _context.Inputs.FirstOrDefault(i => i.Id == id);

                if (inputToDelete == null)
                {
                    return NotFound(); // Return a 404 status code if the Input is not found
                }

                // Remove the Input from the database
                _context.Inputs.Remove(inputToDelete);
                _context.SaveChanges();

                return NoContent(); // Return a 204 status code indicating success
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the Input.");
            }
        }

    }
}