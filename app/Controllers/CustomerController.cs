using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        

    }
}