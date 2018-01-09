using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Backend.Controllers
{
    public class ValuesController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/values/allowed")]
        public IHttpActionResult Allowed()
        {
            return Ok("login okay");
        }

        [Authorize]
        [HttpGet]
        [Route("api/values/logged")]
        public IHttpActionResult Logged()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("hello" + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/values/admin")]
        public IHttpActionResult Admin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("hello" + identity.Name);
        }



        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
