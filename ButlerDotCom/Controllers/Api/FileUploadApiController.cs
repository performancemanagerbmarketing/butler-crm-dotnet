using Butler.Model.Request.FileUpload;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class FileUploadApiController : ApiController
    {

        [HttpPost]
        public object UploadFiles([FromBody] AddFileRequest req)
        {
            req.Id = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetFiles([FromUri] GetFileUploadRequest req)
        {
            req.Id = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    }
}