using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]
    public class ManageProfileController : ControllerBase
    {
        // GET: api/<ManageProfileController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ManageProfileController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManageProfileController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageProfileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageProfileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
