using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Produces("application/json")]
    [Route("values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Gets list of values.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// Gets value with specified id.
        /// </summary>
        /// <param name="id">The id of the value.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// Creates new value.
        /// </summary>
        /// <param name="value">The id of the value.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        /// Edits a value
        /// </summary>
        /// <param name="id">The id of the value.</param>
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public void Put(int id)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// Deletes a value
        /// </summary>
        /// <param name="id">The id of the value.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}