using Butler.Model.Request.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class ServiceRatingApiController : ApiController
    {
        [HttpPost]
        public object AddRating([FromBody] AddServiceRatingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}