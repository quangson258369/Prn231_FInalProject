using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private QuanLyKhoContext _context;
        public CustomerController(QuanLyKhoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.Customers.ToList());
        }
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _context.Customers
                    .ToList();

                return Ok(customers); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving Input records.");
            }
        }

        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer customer)
        {
            try
            {
                customer.Id = 0;
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra trong quá trình tạo khách hàng.");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<List<Customer>> GetCustomerById(string id)
        {
            var customer = _context.OutputInfos
                .Where(input => input.Id == id)
                .ToList();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpDelete("DeleteCustomer/{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                // Tìm khách hàng cần xóa trong cơ sở dữ liệu
                var customerToDelete = _context.Customers.Find(customerId);

                if (customerToDelete == null)
                {
                    return NotFound("Khách hàng không tồn tại."); // Trả về 404 nếu không tìm thấy khách hàng
                }

                _context.Customers.Remove(customerToDelete);
                _context.SaveChanges();

                return NoContent(); // Trả về 204 để thể hiện xóa thành công
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra trong quá trình xóa khách hàng.");
            }
        }


    }
}
