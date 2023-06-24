using Butler.Model.EntityModel;
using Butler.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Notification
{
    public class IsReadNotificationResponse : Response { }
    public class IsReadNotificationRequest
    {
        public int Id { get; set; }
        private ButlerEntities _dbContext = new ButlerEntities();
        public object RunRequest(IsReadNotificationRequest req)
        {
            var response = new IsReadNotificationResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Notification = _dbContext.Notification.Where(x => x.Id == req.Id).FirstOrDefault();
                Notification.IsRead = true;
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message.ToString());
            }
            return response;
        }
    }
}
