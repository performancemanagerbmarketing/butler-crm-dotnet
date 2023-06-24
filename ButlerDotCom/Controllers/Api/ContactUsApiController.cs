using Butler.Model.Request.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class ContactUsApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetContactUsListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddContactUs([FromBody] AddContactUsRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}