using Butler.Model.WorkerAppRequest.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.WorkerAppApi
{
    public class WorkerDashboardApiController : ApiController
    {
        [HttpGet]
        public object Dasboard([FromUri] DashboardRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}