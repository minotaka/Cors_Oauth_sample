using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<string> Get()
        {
            return new string[] {
                "This is a CORS response.",
            };
        }

        [HttpGet]
        [Route("api/values/another")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<string> Another() {
            return new string[] {
                "This is a CORS response.",
                "And route attribute."
            };
        }

        [Authorize]
        [HttpGet]
        [Route("api/values/authorize")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<string> Authorize()
        {
            return new string[] {
                "This is a CORS response.",
                "And authorize."
            };
        }
    }
}
