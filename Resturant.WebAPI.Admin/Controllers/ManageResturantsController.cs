using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageResturantsController : ControllerBase
    {
        // GET: api/<ManageResturantsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ManageResturantsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManageResturantsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageResturantsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageResturantsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
