using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [Produces("application/json")]
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly app.Data.AppContext _context;

        public CustomerController(app.Data.AppContext context)
        {
            _context = context;
        }

        // GET api/customers/5
        /// <summary>
        /// Gets customer with specified id.
        /// </summary>
        /// <param name="id">The id of the customer.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerItem(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        

        // GET api/customers
        /// <summary>
        /// Gets list of customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<app.Models.Customer>> GetCustomers()
        {
            var customersItem =  this._context.Customers;
            
            return customersItem.ToArray();
        }

         // POST api/values
        /// <summary>
        /// Creates new customer.
        /// </summary>
        /// <param name="customer">The id of the value.</param>
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            this._context.Customers.Add(customer);
            await this._context.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetCustomerItem), new { id = customer.ID}, customer);
        }


         // PUT: api/customers/5
         [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
           if(id != customer.ID)
           {
               return BadRequest();
           }

           _context.Entry(customer).State = EntityState.Modified;

           try
           {
               await _context.SaveChangesAsync();
           }catch(DbUpdateConcurrencyException)
           {
            
           }

           return NoContent();
        }

        // DELETE: api/customers/4
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

    }
}