using Butler.Model.Request.CurrentUserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ButlerDotCom.Controllers.Api
{
    public class CurrentLoginUserApiController : ApiController
    {
        [HttpGet]
        public object CurrentLoginUser([FromUri] GetCurrentUserLoginRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    }
}