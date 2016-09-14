using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserManager.Controllers
{
    public class UsersController : ApiController
    {
        [Anonymous]
        public IHttpActionResult Get() {
            return Ok("22");
        }
        public IHttpActionResult Get(string id)
        {
            return Ok("22");
        }
        [Anonymous]
        public IHttpActionResult Post()
        {
            return Ok("post");
        }
    }
}
