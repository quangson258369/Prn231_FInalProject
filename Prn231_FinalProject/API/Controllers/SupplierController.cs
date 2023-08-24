using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private QuanLyKhoContext _context;
        public SupplierController(QuanLyKhoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.Supliers.ToList());
        }
        [HttpGet("GetAllSupplier")]
        public IActionResult GetAllSupplier()
        {
            try
            {
                var sup = _context.Supliers
                    .ToList();

                return Ok(sup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving Input records.");
            }
        }

        [HttpPost("CreateSupplier")]
        public IActionResult CreateSupplier(Suplier sup)
        {
            try
            {
                sup.Id = 0;
                _context.Supliers.Add(sup);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra trong quá trình tạo khách hàng.");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<List<Customer>> GetSupplierById(string id)
        {
            var sup = _context.Objects
                .Where(input => input.Id == id)
                .ToList();
            if (sup == null)
            {
                return NotFound();
            }

            return Ok(sup);
        }

        [HttpDelete("DeleteSupplier/{supplierId}")]
        public IActionResult DeleteSupplier(int supplierId)
        {
            try
            {
                // Tìm khách hàng cần xóa trong cơ sở dữ liệu
                var supplierToDelete = _context.Supliers.Find(supplierId);

                if (supplierToDelete == null)
                {
                    return NotFound("Supplier không tồn tại."); // Trả về 404 nếu không tìm thấy khách hàng
                }
                _context.Supliers.Remove(supplierToDelete);
                _context.SaveChanges();

                return NoContent(); // Trả về 204 để thể hiện xóa thành công
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra trong quá trình xóa Supplier.");
            }
        }

    }
}
