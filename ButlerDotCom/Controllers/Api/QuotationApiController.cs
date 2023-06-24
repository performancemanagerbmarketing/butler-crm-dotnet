using Butler.Model.Request.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class QuotationApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetQuotationListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        
        [HttpPost]
        public object AddQuotation([FromBody] AddQuotationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditQuotation([FromBody] EditQuotationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
       
        [HttpGet]
        public object GetQuotation([FromUri] GetQuotationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}