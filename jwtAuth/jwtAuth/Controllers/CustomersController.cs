using jwtAuth.DTOs;
using jwtAuth.Migrations;
using jwtAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Validater;

namespace jwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly jwtDbContext _context;
        public CustomersController(jwtDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var Customers = await _context.Customers.ToListAsync();
            if (Customers == null) return NotFound();
            return Ok(Customers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            if (id <= 0) return BadRequest();
            Customer? customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> NewCustomer(NewCustomerDto newCustomerDto)
        {
            if (newCustomerDto == null) return BadRequest("Invalid Email Address");
            if(!Validation.ValidateEmail(newCustomerDto.Email)) return BadRequest(); 
            Customer customer = new Customer
            {
                Name = newCustomerDto.Name,
                Email = newCustomerDto.Email,
                Phone = newCustomerDto.Phone,
                Address = newCustomerDto.Address,
                City = newCustomerDto.City,
                StateCode = newCustomerDto.StateCode,
                ZipCode = newCustomerDto.ZipCode,
                RepFirstName = newCustomerDto.RepFirstName,
                RepLastName = newCustomerDto.RepLastName
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomerById", new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, Customer customer)
        {
            if (customer == null) return BadRequest();
            if (id != customer.Id) return BadRequest();
            if (!Validation.ValidateEmail(customer.Email)) return BadRequest();
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok(customer);
        }
    }
}
