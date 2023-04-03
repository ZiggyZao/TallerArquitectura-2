using CustomerWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomerController(CustomerDbContext customerDbContext)
        {
            _context = customerDbContext;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {


            return _context.customers;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _context.customers.FindAsync(customerId);
            return Ok(customer);
        }


        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _context.customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            _context.customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var customer = await _context.customers.FindAsync(customerId);
            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
