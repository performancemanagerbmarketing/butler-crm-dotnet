using Butler.Model.Request.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class ReferenceApiController : ApiController
    {

        [HttpGet]
        public object GetListing([FromUri] GetReferenceListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddReference([FromBody] AddReferenceRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditReference([FromBody] EditReferenceRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetReference([FromUri] GetReferenceRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetReferenceDropdown([FromUri] GetReferenceDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}