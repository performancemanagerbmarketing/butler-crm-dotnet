using Butler.Model.Request.Dashboard;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class DashboardApiController : ApiController
    {
        [HttpGet]
        public object GetDashboardSummary([FromUri] GetDashboardSummaryRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetJobSummaryList([FromUri] GetJobSummaryListRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    }
}