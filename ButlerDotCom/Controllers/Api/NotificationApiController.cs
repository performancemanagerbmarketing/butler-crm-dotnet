using Butler.Model.Request.Notification;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ButlerDotCom.Controllers.Api
{
    public class NotificationApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetNotificationListRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object IsRead([FromBody] IsReadNotificationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}