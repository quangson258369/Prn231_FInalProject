﻿using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private QuanlyKhoContext _context;
        public ProductController(QuanlyKhoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.Objects.Include(o => o.IdUnitNavigation).Include(o => o.IdSuplierNavigation).ToList());
        }
        [HttpGet("{id}")]

        public ActionResult<Models.Object> Get([FromQuery] string id)
        {
            Models.Object? p = _context.Objects.FirstOrDefault(c => c.Id == id);
            if (p is null) return NotFound();
            else return p;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Object p)
        {
            try
            {
                _context.Objects.Add(p);
                int result = _context.SaveChanges();
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
