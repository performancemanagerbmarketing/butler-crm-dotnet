using Butler.Model.Request.ControlCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class ControlCenterApiController : ApiController
    {

        [HttpGet]
        public object GetListing([FromUri] GetControlCenterListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddControlCenter([FromBody] AddControlCenterRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditControlCenter([FromBody] EditControlCenterRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetControlCenter([FromUri] GetControlCenterRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetControlCenterDropdown([FromUri] GetControlCenterDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}