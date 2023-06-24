using Butler.Model.WorkerAppRequest.JobAssign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.WorkerAppApi
{
    public class WorkerJobApiController : ApiController
    {
        [HttpGet]
        public object ActiveJob([FromUri] ActiveJobRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object PreviousJob([FromUri] PreviousJobRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object UpdateJobStatus([FromBody] UpdateJobStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EndJob([FromBody] EndJobRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object PaymentCollection([FromBody] PaymentCollectionRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}