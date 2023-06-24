using Butler.Model.WorkerAppRequest.WorkerAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.WorkerAppApi
{
    public class WorkerAttendanceApiController : ApiController
    {
        [HttpPost]
        public object StartDay([FromBody] StartDayRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EndDay([FromBody] EndDayRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetTodayAttendance([FromUri] GetMarkedAttendanceRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}